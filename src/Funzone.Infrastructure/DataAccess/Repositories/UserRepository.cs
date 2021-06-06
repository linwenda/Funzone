using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FunzoneDbContext _context;

        public UserRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(UserId userId)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}