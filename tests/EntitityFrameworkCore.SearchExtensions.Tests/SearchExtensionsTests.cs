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
        public void SearchWithExpression_WithInvalidPropertySelector_ShouldTrhowArgumentException()
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
            Expression<Func<TestClass, TestClass>> propertySelector = t => t;

            // Act
            //Assert
            Should.Throw<ArgumentException>(() => source.Search(propertySelector, searchTerm));
        }

        [Fact]
        public void SearchWithExpression_WithSearchTearmFoundForString_ShouldReturnItems()
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

        [Fact]
        public void SearchWithExpression_WithSearchTearmFoundForInt_ShouldReturnItems()
        {
            // Arrange
            var intTerm = 98723;
            var searchTerm = intTerm.ToString();

            var randomTestClassFaker = new Faker<TestClass>();
            randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var tests = randomTestClassFaker.Generate(10);

            var testClassFaker = new Faker<TestClass>();
            testClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int() * 100000 + intTerm);

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            var context = new TestDbContext();
            context.Seed();
            context.TestClasses.AddRange(tests);
            context.SaveChanges();

            var source = context.TestClasses;
            Expression<Func<TestClass, int>> propertySelector = testClass => testClass.TestIntProperty;

            // Act
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public void SearchWithExpression_WithSearchTearmNotFoundForString_ShouldNotReturnItems()
        {
            // Arrange
            var searchTerm = "ToatsNotATest";

            var randomTestClassFaker = new Faker<TestClass>();
            randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var tests = randomTestClassFaker.Generate(10);

            var testClassFaker = new Faker<TestClass>();
            testClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
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
            result.ShouldBeEmpty();
        }

        [Fact]
        public void SearchWithExpression_WithSearchTearmNotFoundForInt_ShouldNotReturnItems()
        {
            // Arrange
            var intTerm = 98723;
            var searchTerm = intTerm.ToString();

            var randomTestClassFaker = new Faker<TestClass>();
            randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var tests = randomTestClassFaker.Generate(10);

            var testClassFaker = new Faker<TestClass>();
            testClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            var context = new TestDbContext();
            context.Seed();
            context.TestClasses.AddRange(tests);
            context.SaveChanges();

            var source = context.TestClasses;
            Expression<Func<TestClass, int>> propertySelector = testClass => testClass.TestIntProperty;

            // Act
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBeEmpty();
        }
    }
}
