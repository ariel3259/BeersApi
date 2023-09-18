using BeersApi.Models;
using BeersApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeersApi.Context
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Drinks> Drinks;
        public DbSet<DrinkTypes> DrinkTypes;

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            this.Database.EnsureCreated();
            Drinks = Set<Drinks>();
            DrinkTypes = Set<DrinkTypes>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            setIdName<Drinks>(modelBuilder, "drinks_id");
            setIdName<DrinkTypes>(modelBuilder, "drink_types_id");

            modelBuilder.Entity<DrinkTypes>()
                .HasMany(x => x.Drinks)
                .WithOne(b => b.DrinkType)
                .HasForeignKey(b => b.DrinkTypeId);
        }
        private void setIdName<T>(ModelBuilder modelBuilder, string id) where T : BaseEntity
        {
            modelBuilder.Entity<T>()
                .Property(x => x.Id)
                .HasColumnName(id);
        }
 
    }
}
