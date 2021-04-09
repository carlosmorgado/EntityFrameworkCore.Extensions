using EntitityFrameworkCore.SearchExtensions.Tests.Abstractions;
using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace EntitityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsSqliteTests : SearchExtensionsTests<TestSqliteDbContext>
    {
        protected override TestSqliteDbContext GenerateContext() => new TestSqliteDbContext();
    }
}
