using ManvarFitness.Entity;

namespace ManvarFitness.Interface
{
    public interface IUserRepository
    {
        Task<AdminUser?> GetUserByEmailOrUsernameAsync(string emailUsername);
        Task UpdatePasswordAsync(AdminUser user);

    }
}
