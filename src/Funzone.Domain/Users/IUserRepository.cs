using System.Threading.Tasks;

namespace Funzone.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId userId);
        
        Task AddAsync(User user);
    }
}