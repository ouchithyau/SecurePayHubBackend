using SecurePayHub.Domain.Entities;
using System.Threading.Tasks;

namespace SecurePayHub.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}