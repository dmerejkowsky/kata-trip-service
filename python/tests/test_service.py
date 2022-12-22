from trip_service.service import TripService
from trip_service.user import User
from trip_service.trip import Trip
from trip_service.errors import UserNotLoggedIn

import pytest


class InMemoryRepository:
    def find_trips_by_user(self, user):
        return user.trips


class DisconnectedSession:
    def get_logged_user(self):
        return None


class ConnectedSession:
    def __init__(self, user):
        self.user = user

    def get_logged_user(self):
        return self.user


def test_raise_when_not_logged_in():
    trip_service = TripService(DisconnectedSession(), InMemoryRepository())
    bob = User("Bob")

    with pytest.raises(UserNotLoggedIn):
        trip_service.get_trips_by_user(bob)


def test_return_empty_when_not_a_friend():
    alice = User("Alice")
    trip_service = TripService(ConnectedSession(alice), InMemoryRepository())
    bob = User("Bob")

    trips = trip_service.get_trips_by_user(bob)

    assert not trips


def test_return_trips_of_friend():
    bob = User("Bob")
    alice = User("Alice")
    to_peru = Trip("To Peru")
    alice.add_trip(to_peru)
    alice.add_friend(bob)
    trip_service = TripService(ConnectedSession(bob), InMemoryRepository())

    trips = trip_service.get_trips_by_user(alice)

    assert trips == [to_peru]
