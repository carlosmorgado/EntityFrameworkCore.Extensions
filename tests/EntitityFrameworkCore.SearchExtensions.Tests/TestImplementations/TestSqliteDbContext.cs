using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestSqliteDbContext : DbContext, IDisposable
    {
        private DbConnection connection;

        public TestSqliteDbContext()
            : this(CreateInMemoryConnection())
        {
        }

        private TestSqliteDbContext(DbConnection connection)
            : base(new DbContextOptionsBuilder<TestSqliteDbContext>()
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
