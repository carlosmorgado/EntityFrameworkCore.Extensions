﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Bogus;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations;
using CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions;
using Shouldly;
using Xunit;

namespace CarlosMorgado.EntityFrameworkCore.SearchExtensions.Tests.Abstractions
{
    public abstract class SearchExtensionsTests<TContext> : IDisposable
        where TContext : ITestDbContext
    {
        protected readonly TContext context;

        protected SearchExtensionsTests()
        {
            this.context = this.GenerateContext();
            this.context.Seed();
        }

        protected abstract TContext GenerateContext();

        public virtual void Dispose() => this.context.Dispose();

        [Fact]
        public virtual void SearchWithExpression_WithNullSource_ShouldTrhowArgumentNullException()
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
        public virtual void SearchWithExpression_WithNullPropertySelector_ShouldTrhowArgumentNullException()
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
            Expression<Func<TestClass, string>> propertySelector = null;

            // Act
            //Assert
            Should.Throw<ArgumentNullException>(() => source.Search(propertySelector, searchTerm));
        }

        [Fact]
        public virtual void SearchWithExpression_WithInvalidPropertySelector_ShouldTrhowArgumentException()
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
            Expression<Func<TestClass, TestClass>> propertySelector = t => t;

            // Act
            //Assert
            Should.Throw<ArgumentException>(() => source.Search(propertySelector, searchTerm));
        }

        [Fact]
        public virtual void SearchWithExpression_WithNullSearchTerm_SShouldReturnSource()
        {
            {
                // Arrange
                string searchTerm = null;
                var randomTestClassFaker = new Faker<TestClass>();
                randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
                randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

                var tests = randomTestClassFaker.Generate(10);

                this.context.TestClasses.AddRange(tests);
                this.context.SaveChanges();

                var source = this.context.TestClasses;
                Expression<Func<TestClass, string>> propertySelector = t => t.TestStringProperty;
                var expected = source;

                // Act
                var result = source.Search(propertySelector, searchTerm);

                //Assert
                result.ShouldBe(expected, ignoreOrder: true);
            }
        }

        [Fact]
        public virtual void SearchWithExpression_WithEmptySearchTerm_ShouldReturnSource()
        {
            {
                // Arrange
                var searchTerm = string.Empty;
                var randomTestClassFaker = new Faker<TestClass>();
                randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
                randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

                var tests = randomTestClassFaker.Generate(10);

                this.context.TestClasses.AddRange(tests);
                this.context.SaveChanges();

                var source = this.context.TestClasses;
                Expression<Func<TestClass, string>> propertySelector = t => t.TestStringProperty;
                var expected = source;

                // Act
                var result = source.Search(propertySelector, searchTerm);

                //Assert
                result.ShouldBe(expected, ignoreOrder: true);
            }
        }

        [Fact]
        public virtual void SearchWithExpression_WithSearchTearmFoundForString_ShouldReturnItems()
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
            Expression<Func<TestClass, string>> propertySelector = testClass => testClass.TestStringProperty;

            // Act
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public virtual void SearchWithExpression_WithSearchTearmFoundForInt_ShouldReturnItems()
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
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public virtual void SearchWithExpression_WithSearchTearmNotFoundForString_ShouldNotReturnItems()
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
            Expression<Func<TestClass, string>> propertySelector = testClass => testClass.TestStringProperty;

            // Act
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public virtual void SearchWithExpression_WithSearchTearmNotFoundForInt_ShouldNotReturnItems()
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
            var result = source.Search(propertySelector, searchTerm);

            //Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public virtual void Search_WithNullSource_ShouldTrhowArgumentNullException()
        {
            // Arrange
            IQueryable<TestClass> source = null;
            var searchTerm = "ToatsNotATest";

            // Act
            //Assert
            Should.Throw<ArgumentNullException>(() => source.Search(searchTerm));
        }

        [Fact]
        public virtual void Search_WithNullSearchTerm_SShouldReturnSource()
        {
            {
                // Arrange
                string searchTerm = null;
                var randomTestClassFaker = new Faker<TestClass>();
                randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
                randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

                var tests = randomTestClassFaker.Generate(10);

                this.context.TestClasses.AddRange(tests);
                this.context.SaveChanges();

                var source = this.context.TestClasses;
                var expected = source;

                // Act
                var result = source.Search(searchTerm);

                //Assert
                result.ShouldBe(expected, ignoreOrder: true);
            }
        }

        [Fact]
        public virtual void Search_WithEmptySearchTerm_ShouldReturnSource()
        {
            {
                // Arrange
                var searchTerm = string.Empty;
                var randomTestClassFaker = new Faker<TestClass>();
                randomTestClassFaker.RuleFor(testClass => testClass.TestStringProperty, faker => faker.Lorem.Sentence());
                randomTestClassFaker.RuleFor(testClass => testClass.TestIntProperty, faker => faker.Random.Int());

                var tests = randomTestClassFaker.Generate(10);

                this.context.TestClasses.AddRange(tests);
                this.context.SaveChanges();

                var source = this.context.TestClasses;
                var expected = source;

                // Act
                var result = source.Search(searchTerm);

                //Assert
                result.ShouldBe(expected, ignoreOrder: true);
            }
        }

        [Fact]
        public virtual void Search_WithSearchTearmFoundForString_ShouldReturnItems()
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
            var result = source.Search(searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public virtual void Search_WithSearchTearmFoundForInt_ShouldReturnItems()
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
            var result = source.Search(searchTerm);

            //Assert
            result.ShouldBe(expected, ignoreOrder: true);
        }

        [Fact]
        public virtual void Search_WithSearchTearmNotFoundForString_ShouldNotReturnItems()
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
            var result = source.Search(searchTerm);

            //Assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public virtual void Search_WithSearchTearmNotFoundForInt_ShouldNotReturnItems()
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
            var result = source.Search(searchTerm);

            //Assert
            result.ShouldBeEmpty();
        }
    }
}
