namespace DiyOmnitheca.Test.Mocks
{
    using DiyOmnitheca.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DataBaseMock
    {
        public static OmnithecaDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<OmnithecaDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new OmnithecaDbContext(dbContextOptions);
            }
        }
    }
}
