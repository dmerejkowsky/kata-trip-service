from trip_service.user_session import session
from trip_service.errors import UserNotLoggedIn
from trip_service.trip_dao import TripDAO


class TripService:
    def __init__(self, user_session, trip_repository):
        self.user_session = user_session
        self.trip_repository = trip_repository

    def get_trips_by_user(self, other_user):
        logged_user = self.user_session.get_logged_user()
        if not logged_user:
            raise UserNotLoggedIn()

        if other_user.is_friends_with(logged_user):
            return self.trip_repository.find_trips_by_user(other_user)
        else:
            return []
