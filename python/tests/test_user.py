from trip_service.user import User
from trip_service.trip import Trip


def test_users_can_have_friends():
    alice = User("Alice")
    bob = User("Bob")

    alice.add_friend(bob)

    assert alice.friends == [bob]


def test_users_have_a_list_of_trips():
    alice = User("Alice")
    to_peru = Trip("Exciting trip to Peru")

    alice.add_trip(to_peru)

    assert alice.trips == [to_peru]
