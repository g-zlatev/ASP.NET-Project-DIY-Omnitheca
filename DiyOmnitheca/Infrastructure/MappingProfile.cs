namespace DiyOmnitheca.Infrastructure
{
    using AutoMapper;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Models.Products;
    using DiyOmnitheca.Services.Products;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, LatestProductServiceModel>();
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Lender.UserId));
        }
    }
}
