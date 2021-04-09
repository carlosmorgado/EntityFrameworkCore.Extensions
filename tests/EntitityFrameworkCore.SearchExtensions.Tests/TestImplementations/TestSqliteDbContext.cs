using System.Data.Common;
using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestSqliteDbContext : ADbContext
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
