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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<RolePage> RolePages { get; set; }
        public DbSet<Concerns> Concerns { get; set; }
        public DbSet<HerbCategory> HerbCategories { get; set; }
        public DbSet<DietPlan> DietPlans{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubConcerns> SubConcerns { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<FormSubmission> FormSubmissions { get; set; }
    }
}
