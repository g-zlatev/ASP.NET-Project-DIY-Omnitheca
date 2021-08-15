namespace DiyOmnitheca.Services.Lenders
{
    public interface ILenderService
    {
        public bool IsLender(string userId);

        public int GetIdByUser(string UserId);
    }
}
