using MassTransit;
using SecurePayHub.Application.Contracts;
using SecurePayHub.Application.DTOs;
using SecurePayHub.Application.Interfaces;
using SecurePayHub.Domain.Entities;
using SecurePayHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;


namespace SecurePayHub.Application.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(IUnitOfWork unitOfWork, ICacheService cacheService, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            await _cacheService.RemoveCacheAsync("products_list");
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            const string cacheKey = "products_list";

          
            var cachedData = await _cacheService.GetCacheAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<Product>>(cachedData) ?? new List<Product>();
            }

            
            return new List<Product>();
        }

        public async Task<string> CreateOrderAsync(int userId, CreateOrderRequest request)
        {
            // ACID Transaction 
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                if (product.Stock < request.Quantity)
                {
                    throw new Exception("Insufficient stock available.");
                }

                
                product.Stock -= request.Quantity;
                await _unitOfWork.Products.UpdateAsync(product);

               
                var order = new Order
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    TotalPrice = product.Price * request.Quantity,
                    OrderDate = DateTime.UtcNow
                };

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.CompleteAsync();


                await _unitOfWork.CommitTransactionAsync();
                await _publishEndpoint.Publish<OrderCreatedEvent>(new
                {
                    OrderId = order.Id,
                    UserId = userId,
                    request.ProductId,
                    request.Quantity,
                    order.TotalPrice,
                    CreatedAt = DateTime.UtcNow
                });

                return "Order placed successfully!";
            }
            catch (Exception)
            {
                
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}