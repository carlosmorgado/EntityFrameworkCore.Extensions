using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.Abstractions;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsSqlServerTests : SearchExtensionsTests<TestSqlServerDbContext>
    {
        protected override TestSqlServerDbContext GenerateContext() => new TestSqlServerDbContext();
    }
}
