namespace DiyOmnitheca.Data
{
    using DiyOmnitheca.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class OmnithecaDbContext : IdentityDbContext
    {

        public OmnithecaDbContext(DbContextOptions<OmnithecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Product>()
            //    .HasMany(c => c.Categories)
            //    .WithMany(p => p.Products)
            //    .UsingEntity<Dictionary<string, object>>(
            //    "ProductCategories",
            //    c => c.HasOne<Category>().WithMany().OnDelete(DeleteBehavior.Restrict),
            //    c => c.HasOne<Product>().WithMany().OnDelete(DeleteBehavior.Restrict));


            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
