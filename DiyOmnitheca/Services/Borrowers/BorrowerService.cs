namespace DiyOmnitheca.Services.Borrowers
{
    using DiyOmnitheca.Data;
    using System.Linq;

    public class BorrowerService : IBorrowerService
    {
        private readonly OmnithecaDbContext data;

        public BorrowerService(OmnithecaDbContext data)
            => this.data = data;

        public int IdByUser(string userId)
            => this.data
                   .Borrowers
                   .Where(b => b.UserId == userId)
                   .Select(b => b.Id)
                   .FirstOrDefault();

        public bool IsBorrower(string userId)
            => this.data
                .Borrowers
                .Any(b => b.UserId == userId);
    }
}
