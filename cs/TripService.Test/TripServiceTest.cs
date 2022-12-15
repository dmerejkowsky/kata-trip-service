
namespace TripService.Test;

public class TripServiceTest
{
    private static User loggedInUser = new User("Bob");
    private static User otherUser = new User("Charlie");
    private static Trip toPeru = new Trip("Exciting trip to Peru");

    class TripServiceWithLoggedInUser : TripService
    {
        override protected User GetLoggedUser()
        {
            return loggedInUser;
        }
        protected override List<Trip> GetTripsForUser(User user)
        {
            return user.Trips;
        }
    }

    class TripServiceWithLoggedOutUser : TripService
    {
        override protected User? GetLoggedUser()
        {
            return null;
        }
        protected override List<Trip> GetTripsForUser(User user)
        {
            return user.Trips;
        }
    }

    [Test]
    public void should_throw_when_not_logged_in()
    {
        var tripService = new TripServiceWithLoggedOutUser();
        Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(otherUser));
    }

    [Test]
    public void should_return_empty_list_when_other_user_is_not_a_friend()
    {
        var tripService = new TripServiceWithLoggedInUser();

        var trips = tripService.GetTripsByUser(otherUser);

        Assert.That(trips, Is.Empty);
    }

    [Test]
    public void should_return_trips_of_friends()
    {
        otherUser.AddFriend(loggedInUser);
        otherUser.AddTrip(toPeru);
        var tripService = new TripServiceWithLoggedInUser();

        var trips = tripService.GetTripsByUser(otherUser);

        Assert.That(trips, Is.EqualTo(new[] { toPeru }));
    }
}
