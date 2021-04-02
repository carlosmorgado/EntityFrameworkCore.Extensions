using System;
using System.Linq;
using System.Linq.Expressions;
using Bogus;
using EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations;
using Shouldly;
using Xunit;

namespace EntitityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsTests
    {
        [Fact]
        public void SearchWithExpression_WithNullSource_ShouldTrhowArgumentNullException()
        {
            // Arrange
            IQueryable<TestClass> source = null;
            Expression<Func<TestClass, string>> propertySelector = testClass => testClass.TestStringProperty;
            var searchTerm = "ToatsNotATest";

            // Act
            //Assert
            Should.Throw<ArgumentNullException>(() => source.Search(propertySelector, searchTerm));
        }

        [Fact]
        public void SearchWithExpression_WithNullPropertySelector_ShouldTrhowArgumentNullException()
        {
            // Arrange
            var searchTerm = "ToatsNotATest";
            var randomTestClassFaker = new Faker<TestClass>();
            randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var tests = randomTestClassFaker.Generate(10);

            var testClassFaker = new Faker<TestClass>();
            testClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => searchTerm + faker.Lorem.Sentence());
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            var context = new TestDbContext();
            context.Seed();
            context.TestClasses.AddRange(tests);
            context.SaveChanges();

            var source = context.TestClasses;
            Expression<Func<TestClass, string>> propertySelector = null;

            // Act
            //Assert
            Should.Throw<ArgumentNullException>(() => source.Search(propertySelector, searchTerm));
        }

        [Fact]
        public void SearchWithExpression_WithSearchTearmFound_ShouldReturnItems()
        {
            // Arrange
            var searchTerm = "ToatsNotATest";

            var randomTestClassFaker = new Faker<TestClass>();
            randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var tests = randomTestClassFaker.Generate(10);

            var testClassFaker = new Faker<TestClass>();
            testClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => searchTerm + faker.Lorem.Sentence());
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            var context = new TestDbContext();
            context.Seed();
            context.TestClasses.AddRange(tests);
            context.SaveChanges();

            var source = context.TestClasses;
            Expression<Func<TestClass, string>> propertySelector = testClass => testClass.TestStringProperty;

            // Act
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }
    }
}
