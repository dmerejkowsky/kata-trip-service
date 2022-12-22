class User:
    def __init__(self, name):
        self.name = name
        self.trips = []
        self.friends = []

    def add_trip(self, trip):
        self.trips.append(trip)

    def add_friend(self, user):
        self.friends.append(user)

    def __repr__(self):
        return f"User({self.name})"
