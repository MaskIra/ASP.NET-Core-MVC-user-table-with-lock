using Microsoft.EntityFrameworkCore;


namespace WebForUsers.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Status blocked = new Status { Id = 1, Name = "blocked" };
            Status unblocked = new Status { Id = 2, Name = "unblocked" };
            modelBuilder.Entity<Status>().HasData(new Status[] { blocked, unblocked });
            base.OnModelCreating(modelBuilder);
        }
    }
}
