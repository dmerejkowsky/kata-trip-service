namespace TripService;

public class TripService
{
    public List<Trip> GetTripsByUser(User user)
    {
        List<Trip> tripList = new List<Trip>();
        User loggedUser = GetLoggedUser();
        bool isFriend = false;
        if (loggedUser != null)
        {
            foreach (User friend in user.Friends)
            {
                if (friend.Equals(loggedUser))
                {
                    isFriend = true;
                    break;
                }
            }
            if (isFriend)
            {
                tripList = GetTripsForUser(user);
            }
            return tripList;
        }
        else
        {
            throw new UserNotLoggedInException();
        }
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
