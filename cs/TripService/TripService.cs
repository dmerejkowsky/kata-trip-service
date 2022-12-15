namespace TripService;

public class TripService
{
    public List<Trip> GetTripsByUser(User user)
    {
        User? loggedUser = GetLoggedUser();
        ValidateUser(loggedUser);

        return user.IsFriendsWith(loggedUser!) ? GetTripsForUser(user) : new List<Trip>();
    }


    private static void ValidateUser(User? loggedUser)
    {
        if (loggedUser == null) 
            throw new UserNotLoggedInException();     
    }

    virtual protected List<Trip> GetTripsForUser(User user)
    {
        return TripDAO.FindTripsByUser(user);
    }

    virtual protected User? GetLoggedUser()
    {
        return UserSession.GetInstance().GetLoggedUser();
    }
}
