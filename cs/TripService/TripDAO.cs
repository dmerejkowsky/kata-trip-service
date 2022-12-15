namespace TripService;

public class TripDAO : IRepository
{
    public List<Trip> GetTripsForUser(User user)
    {
        throw new Exception("Cannot use TripDAO for unit tests");
    }
}