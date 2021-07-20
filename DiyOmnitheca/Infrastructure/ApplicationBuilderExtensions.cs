namespace DiyOmnitheca.Infrastructure
{
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<OmnithecaDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }


        private static void SeedCategories(OmnithecaDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Electric"},
                new Category {Name = "Home appliances"},
                new Category {Name = "Handheld"},
                new Category {Name = "Heavy duty"},
                new Category {Name = "Fine work"},
                new Category {Name = "Professional"},
            });

            data.SaveChanges();
        }
    }
}
