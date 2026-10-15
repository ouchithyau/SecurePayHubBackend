using Microsoft.EntityFrameworkCore;
using SecurePayHub.Domain.Entities;
using SecurePayHub.Domain.Interfaces;
using SecurePayHub.Infrastructure.Data;
using System.Threading.Tasks;

namespace SecurePayHub.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);
        public Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }
    }
}