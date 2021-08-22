namespace DiyOmnitheca.Test.Mocks
{
    using AutoMapper;
    using DiyOmnitheca.Infrastructure;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(conf =>
                {
                    conf.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
        }
    }
}
}
