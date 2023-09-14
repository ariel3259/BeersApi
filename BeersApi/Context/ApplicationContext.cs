using BeersApi.Models;
using BeersApi.Models.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeersApi.Context
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Beers> Beers;
        public DbSet<BeerTypes> BeerTypes;
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            this.Database.EnsureCreated();
            Beers = Set<Beers>();
            BeerTypes = Set<BeerTypes>();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            setIdName<Beers>(modelBuilder, "beers_id");
            setIdName<BeerTypes>(modelBuilder, "beer_types_id");

            modelBuilder.Entity<BeerTypes>()
                .HasMany(x => x.Beers)
                .WithOne(b => b.BeerTypes)
                .HasForeignKey(b => b.BeerTypeId);
        }
        private void setIdName<T>(ModelBuilder modelBuilder, string id) where T : BaseEntity
        {
            modelBuilder.Entity<T>()
                .Property(x => x.Id)
                .HasColumnName(id);
        }
 
    }
}
