using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations
{
    public class TestPostgreSqlDbContext : ADbContext
    {
        private const string connectionString = "Host=localhost;Database=SearchExtentionsTestDatabase;Username=postgres;Password=MySuperStrongPassword!";

        public TestPostgreSqlDbContext()
            : base(new DbContextOptionsBuilder<TestPostgreSqlDbContext>()
                .UseNpgsql(connectionString)
                .Options)
        {
        }
    }
}
