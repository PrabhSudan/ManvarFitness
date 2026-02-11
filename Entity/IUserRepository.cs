namespace ManvarFitness.Entity
{
    public interface IUserRepository
    {
        Task<AdminUser?> GetUserByEmailOrUsernameAsync(string emailUsername);
        Task UpdatePasswordAsync(AdminUser user);
    }
}
