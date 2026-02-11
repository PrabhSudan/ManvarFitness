using ManvarFitness.Entity;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Database
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; } 
        public DbSet<WorkoutVideo> WorkoutVideos { get; set; }
    }
}
