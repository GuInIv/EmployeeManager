using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.ServerApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Position>(p => p.Position)
                .WithMany(s => s.Employees)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => new { e.FirstName, e.LastName, e.TerminationDate }).IsUnique().HasFilter(null);

            modelBuilder.Entity<Position>()
                .HasIndex(p => p.Name).IsUnique();
        }
    }
}
