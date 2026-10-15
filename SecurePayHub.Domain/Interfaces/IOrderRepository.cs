using SecurePayHub.Domain.Entities;
using System.Threading.Tasks;

namespace SecurePayHub.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}