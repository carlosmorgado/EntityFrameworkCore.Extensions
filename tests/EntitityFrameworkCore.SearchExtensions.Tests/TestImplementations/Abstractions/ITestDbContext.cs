using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntitityFrameworkCore.SearchExtensions.Tests.TestImplementations.Abstractions
{
    public interface ITestDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();

        void Seed();

        public DbSet<TestClass> TestClasses { get; set; }
    }
}
