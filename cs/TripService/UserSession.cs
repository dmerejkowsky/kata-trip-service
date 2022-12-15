using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace TripService;

public class UserSession
{
    private static readonly UserSession INSTANCE = new UserSession();
    private UserSession() { }

    public static UserSession GetInstance() => INSTANCE;

    public User GetLoggedUser()
    {
        throw new Exception("Cannot use UserSession for unit tests");
    }

    public bool IsLoggedIn(User user)
    {
        throw new Exception("Cannot use UserSession for unit tests");
    }

}
