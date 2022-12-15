namespace TripService;

public interface IRepository
{
    List<Trip> GetTripsForUser(User user);
}
