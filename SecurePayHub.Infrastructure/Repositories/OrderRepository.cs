using SecurePayHub.Domain.Entities;
using SecurePayHub.Domain.Interfaces;
using SecurePayHub.Infrastructure.Data;
using System.Threading.Tasks;

namespace SecurePayHub.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);
    }
}