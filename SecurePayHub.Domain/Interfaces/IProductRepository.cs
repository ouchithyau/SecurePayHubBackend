using SecurePayHub.Domain.Entities;
using System.Threading.Tasks;

namespace SecurePayHub.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}