namespace TripService;

public class TripService
{
    private readonly ISession _session;
    private readonly IRepository _repository;
    public TripService(ISession session, IRepository repository)
    {
        _session = session;
        _repository = repository;
    }

    public List<Trip> GetFriendTrips(User user)
    {
        User? loggedUser = _session.GetLoggedUser();
        if (loggedUser == null)
            throw new UserNotLoggedInException();

        return user.IsFriendsWith(loggedUser) ? _repository.GetTripsForUser(user) : new List<Trip>();
    }
}
