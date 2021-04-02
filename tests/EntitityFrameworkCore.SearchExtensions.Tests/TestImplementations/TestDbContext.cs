using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestDbContext : DbContext, IDisposable
    {
        private DbConnection connection;

        public TestDbContext()
            : this(CreateInMemoryConnection())
        {
        }

        private TestDbContext(DbConnection connection)
            : base(new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite(connection)
                .Options)
        {
            this.connection = connection;
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

        public override void Dispose()
        {
            this.connection.Dispose();
            base.Dispose();
        }

        private static DbConnection CreateInMemoryConnection()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
