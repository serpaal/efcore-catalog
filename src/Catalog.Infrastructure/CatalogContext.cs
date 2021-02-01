using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Repositories;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.SchemaDefinitions;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "catalog";

        public DbSet<Item> Items { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) :
         base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ArtistEntitySchemaConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntitySchemaConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
