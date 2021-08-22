namespace DiyOmnitheca.Services.Borrowers
{
    public interface IBorrowerService
    {
        public bool IsBorrower(string userId);

        public int IdByUser(string userId);
    }
}
