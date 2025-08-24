using Microsoft.EntityFrameworkCore;
using WebWorker.Data.Entities.Identity;

namespace WebWorker.DAL.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserEntity> GetAll()
            => _context.Users.Include(u => u.UserRoles);
    }
}
