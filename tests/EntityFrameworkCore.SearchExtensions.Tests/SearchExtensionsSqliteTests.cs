using System;
using System.Linq;
using System.Linq.Expressions;
using Bogus;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.Abstractions;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations;
using Shouldly;
using Xunit;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests
{
    public class SearchExtensionsSqliteTests : SearchExtensionsTests<TestSqliteDbContext>
    {
        protected override TestSqliteDbContext GenerateContext() => new TestSqliteDbContext();

        [Fact]
        // Actually throws exception.
        public override void Search_WithSearchTearmFoundForInt_ShouldReturnItems()
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
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int(0, 10) * 100000 + intTerm);

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(searchTerm).ToList());
        }

        [Fact]
        // Actually throws exception.
        public override void Search_WithSearchTearmFoundForString_ShouldReturnItems()
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

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(searchTerm).ToList());
        }

        [Fact]
        // Actually throws exception.
        public override void Search_WithSearchTearmNotFoundForInt_ShouldNotReturnItems()
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

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(searchTerm).ToList());
        }

        [Fact]
        // Actually throws exception.
        public override void Search_WithSearchTearmNotFoundForString_ShouldNotReturnItems()
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

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(searchTerm).ToList());
        }

        [Fact]
        // Actually throws exception.
        public override void SearchWithExpression_WithSearchTearmFoundForInt_ShouldReturnItems()
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
            testClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int(0, 10) * 100000 + intTerm);

            var expected = testClassFaker.Generate(10);

            tests.AddRange(expected);

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;
            Expression<Func<TestClass, int>> propertySelector = testClass => testClass.TestIntProperty;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(propertySelector, searchTerm).ToList());
        }

        [Fact]
        // Actually throws exception.
        public override void SearchWithExpression_WithSearchTearmNotFoundForInt_ShouldNotReturnItems()
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

            this.context.TestClasses.AddRange(tests);
            this.context.SaveChanges();

            var source = this.context.TestClasses;
            Expression<Func<TestClass, int>> propertySelector = testClass => testClass.TestIntProperty;

            // Act
            //Assert
            Should.Throw<InvalidOperationException>(() => source.Search(propertySelector, searchTerm).ToList());
        }
    }
}
