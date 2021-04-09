using EntitityFrameworkCore.SearchExtensions.Tests.Abstractions;
using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace EntitityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsSqlServerTests : SearchExtensionsTests<TestSqlServerDbContext>
    {
        protected override TestSqlServerDbContext GenerateContext() => new TestSqlServerDbContext();
    }
}
