namespace BookStore.Services
{
    public interface IUserService
    {
        string GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();
    }
}