using EntitityFrameworkCore.SearchExtensions.Tests.Abstractions;
using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace EntitityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsPostgreSqlTests : SearchExtensionsTests<TestPostgreSqlDbContext>
    {
        protected override TestPostgreSqlDbContext GenerateContext() => new TestPostgreSqlDbContext();
    }
}
