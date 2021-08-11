namespace DiyOmnitheca.Data
{
    using DiyOmnitheca.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class OmnithecaDbContext : IdentityDbContext
    {

        public OmnithecaDbContext(DbContextOptions<OmnithecaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Lender> Lenders { get; init;}

        public DbSet<Borrower> Borrowers { get; init; }


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

            builder
                .Entity<Lender>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Lender>(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Lender)
                .WithMany(l => l.Products)
                .HasForeignKey(p => p.LenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Borrower>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Borrower>(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(p => p.Borrower)
                .WithMany(b => b.BorrowedProducts)
                .HasForeignKey(p => p.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
