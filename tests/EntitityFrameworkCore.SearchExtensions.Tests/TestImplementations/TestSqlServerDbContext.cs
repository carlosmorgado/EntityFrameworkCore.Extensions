using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestSqlServerDbContext : ADbContext
    {
        private const string connectionString = "Server=localhost;Database=SearchExtentionsTestDatabase;User Id=SA;Password=MySuperStrongPassword!;";

        public TestSqlServerDbContext()
            : base(new DbContextOptionsBuilder<TestSqlServerDbContext>()
                .UseSqlServer(connectionString)
                .Options)
        {
        }
    }
}
