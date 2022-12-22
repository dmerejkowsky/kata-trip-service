class User:
    def __init__(self, name):
        self.name = name
        self.trips = []
        self.friends = []

    def add_trip(self, trip):
        self.trips.append(trip)

    def add_friend(self, user):
        self.friends.append(user)

    def is_friends_with(self, other_user):
        return other_user in self.friends

    def __repr__(self):
        return f"User({self.name})"
