using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.Abstractions;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsPostgreSqlTests : SearchExtensionsTests<TestPostgreSqlDbContext>
    {
        protected override TestPostgreSqlDbContext GenerateContext() => new TestPostgreSqlDbContext();
    }
}
