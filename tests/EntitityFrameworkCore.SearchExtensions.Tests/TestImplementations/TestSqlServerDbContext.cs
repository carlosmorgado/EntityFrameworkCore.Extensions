using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestSqlServerDbContext : DbContext, IDisposable
    {
        private const string connectionString = "Server=localhost;Database=SearchExtentionsTestDatabase;User Id=SA;Password=MySuperStrongPassword!;";

        public TestSqlServerDbContext()
            : base(new DbContextOptionsBuilder<TestSqlServerDbContext>()
                .UseSqlServer(connectionString)
                .Options)
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
