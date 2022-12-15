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

    public List<Trip> GetTripsByUser(User user)
    {
        User? loggedUser = _session.GetLoggedUser();
        ValidateUser(loggedUser);

        return user.IsFriendsWith(loggedUser!) ? _repository.GetTripsForUser(user) : new List<Trip>();
    }


    private static void ValidateUser(User? loggedUser)
    {
        if (loggedUser == null)
            throw new UserNotLoggedInException();
    }
}
