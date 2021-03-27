using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EntitityFrameworkCore.SearchExtensions.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var source = new List<TestClass>
            {
                new TestClass
                {
                    A = "qwerty",
                    B = "asdfgh"
                }
            }.AsQueryable();

            var searchTearm = "a";

            // Act
            var result = SearchExtensions.Search(source, searchTearm);

            //Assert

        }

        private class TestClass
        {
            public string A { get; set; }

            public string B { get; set; }
        }
    }
}
