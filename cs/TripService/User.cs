namespace TripService;

public class User
{
    public readonly List<User> Friends;
    public readonly List<Trip> Trips;

    public string Name { get;  }
    public User(string name)
    {
        Name = name;
        Friends = new List<User>();
        Trips = new List<Trip>();
    }

    public void AddFriend(User friend)
    {
        Friends.Add(friend);
    }


    public void AddTrip(Trip trip)
    {
        Trips.Add(trip);
    }
    public override string ToString()
    {
        return $"User[{Name}]>";
    }
}
