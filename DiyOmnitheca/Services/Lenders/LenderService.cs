namespace DiyOmnitheca.Services.Lenders
{
    using System.Linq;
    using DiyOmnitheca.Data;

    public class LenderService : ILenderService
    {
        private readonly OmnithecaDbContext data;

        public LenderService(OmnithecaDbContext data)
            => this.data = data;

        public bool IsLender(string userId)
            => this.data
            .Lenders
            .Any(l => l.UserId == userId);

        public int IdByUser(string userId)
             => this.data
                   .Lenders
                   .Where(l => l.UserId == userId)
                   .Select(l => l.Id)
                   .FirstOrDefault();

    }
}
