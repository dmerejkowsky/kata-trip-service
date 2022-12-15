namespace TripService
{
    public interface ISession
    {
        User? GetLoggedUser();

        bool IsLoggedIn(User user);
    }
}
