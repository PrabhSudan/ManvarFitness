using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Interface;
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
        public async Task<AdminUser?> GetUserByEmailOrUsernameAsync(string Email)
        {
            return await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Email == Email);    
        }

        public async Task UpdatePasswordAsync(AdminUser user)
        {
            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();
        }


    }
}
