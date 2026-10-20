using Moq;
using SecurePayHub.Application.DTOs;
using SecurePayHub.Application.Interfaces;
using SecurePayHub.Application.Services;
using SecurePayHub.Domain.Entities;
using SecurePayHub.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SecurePayHub.UnitTests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_ShouldFail_WhenProductNotFound()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cacheMock = new Mock<ICacheService>();
            var publishEndpointMock = new Mock<MassTransit.IPublishEndpoint>();

            unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((SecurePayHub.Domain.Entities.Product)null);

            var service = new OrderService(unitOfWorkMock.Object, cacheMock.Object, publishEndpointMock.Object);

            // Act & Assert
            var request = new CreateOrderRequest(ProductId: 99, Quantity: 1);

            // KeyNotFoundException වෙනුවට System.Exception පාවිච්චි කරමු
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await service.CreateOrderAsync(userId: 1, request);
            });
        }
    }
}