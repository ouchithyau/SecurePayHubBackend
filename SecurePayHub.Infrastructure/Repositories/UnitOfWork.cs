using Microsoft.EntityFrameworkCore.Storage;
using SecurePayHub.Domain.Interfaces;
using SecurePayHub.Infrastructure.Data;
using System.Threading.Tasks;

namespace SecurePayHub.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(AppDbContext context, IProductRepository products, IOrderRepository orders)
        {
            _context = context;
            Products = products;
            Orders = orders;
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}