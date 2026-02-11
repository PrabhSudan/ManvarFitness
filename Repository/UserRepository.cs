using ManvarFitness.Database;
using ManvarFitness.Entity;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; 
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdminUser?> GetUserByEmailOrUsernameAsync(string emailUsername)
        {
            return await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.EmailUsername == emailUsername);    
        }

        public async Task UpdatePasswordAsync(AdminUser user)
        {
            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
