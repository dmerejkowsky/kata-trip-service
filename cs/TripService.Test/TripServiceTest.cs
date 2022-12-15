
namespace TripService.Test;

public class RejectingSession : ISession
{
    public User? GetLoggedUser() => null;

    public bool IsLoggedIn(User user) => false;
}

public class AcceptingSession : ISession
{
    public User User { get ; }

    public AcceptingSession(User user)
    {
        User = user;
    }

    public User? GetLoggedUser() => User;

    public bool IsLoggedIn(User user) => true;
}

public class FakeRepository : IRepository
{
    public List<Trip> GetTripsForUser(User user) => user.Trips;
}
public class TripServiceTest
{
    private static User loggedInUser = new User("Bob");
    private static User otherUser = new User("Charlie");
    private static Trip toPeru = new Trip("Exciting trip to Peru");
    private readonly FakeRepository _repository;

    public TripServiceTest()
    {
        _repository = new FakeRepository();
    }

    TripService ServiceForLoggetOutUser() => new TripService(new RejectingSession(), _repository);

    TripService ServiceForLoggedInUser() => new TripService(new AcceptingSession(loggedInUser), _repository);


    [Test]
    public void should_throw_when_not_logged_in()
    {
        var tripService = ServiceForLoggetOutUser();
        Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(otherUser));
    }

    [Test]
    public void should_return_empty_list_when_other_user_is_not_a_friend()
    {
        var tripService = ServiceForLoggedInUser();

        var trips = tripService.GetTripsByUser(otherUser);

        Assert.That(trips, Is.Empty);
    }

    [Test]
    public void should_return_trips_of_friends()
    {
        var tripService = ServiceForLoggedInUser();
        otherUser.AddFriend(loggedInUser);
        otherUser.AddTrip(toPeru);

        var trips = tripService.GetTripsByUser(otherUser);

        Assert.That(trips, Is.EqualTo(new[] { toPeru }));
    }
}
