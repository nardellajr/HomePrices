using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using HomePrices.data;
using Moq.AutoMock.Resolvers;
using System.Linq;
using System.Net;
using System.IO;
using System;

namespace HomePrices.Tests;

public static class AutoMockerExtensions
{

    public static IDbScope<HomePricesContext> WithDBScope(this AutoMocker mocker)
    {
        var resolver = new DbScopedResolver();
        var existing = mocker.Resolvers.ToList();
        mocker.Resolvers.Clear();
        existing.Insert(0, resolver);
        existing.Add(resolver);
        foreach (var item in existing)
        {
            mocker.Resolvers.Add(item);
        }
        
        return resolver;
    }

    public interface IDbScope<TContext> : IDbContextFactory<TContext>, IDisposable
        where TContext : DbContext
    {
    }

    private sealed class DbScopedResolver : IMockResolver, IDbScope<HomePricesContext>
    {
        private bool _disposedValue;

        private string FilePath { get; }

        public DbScopedResolver()
        {
            FilePath = Path.Combine(
                Path.GetTempPath(),
                "HomePricesTests",
                Guid.NewGuid().ToString("N")
                );
            
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

            using var context = CreateDbContext();
            context.Database.Migrate();
        }

        public void Resolve(MockResolutionContext context)
        {
            if (context.RequestType == typeof(HomePricesContext))
            {
                context.Value = CreateDbContext();
            }
            else if (context.RequestType == typeof(Func<HomePricesContext>))
            {
                context.Value = new Func<HomePricesContext>(CreateDbContext);
            }
        }

        public HomePricesContext CreateDbContext()
        {

            var connectionString = new SqliteConnectionStringBuilder
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = FilePath,
                Pooling = false,
            };            
            
            var options = new DbContextOptionsBuilder<HomePricesContext>()
                .UseSqlite(connectionString.ToString()).Options;

            return new HomePricesContext(options);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }


}
