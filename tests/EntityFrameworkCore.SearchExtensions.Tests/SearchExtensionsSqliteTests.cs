using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.Abstractions;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsSqliteTests : SearchExtensionsTests<TestSqliteDbContext>
    {
        protected override TestSqliteDbContext GenerateContext() => new TestSqliteDbContext();
    }
}
