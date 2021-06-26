using Microsoft.EntityFrameworkCore;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions
{
    public abstract class ADbContext : DbContext, ITestDbContext
    {
        protected ADbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TestClass> TestClasses { get; set; }

        public void Seed()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TestClass>()
                .HasKey(testClass => testClass.Id);
        }
    }
}
