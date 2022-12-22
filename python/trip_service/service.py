from trip_service.user_session import session
from trip_service.errors import UserNotLoggedIn
from trip_service.trip_dao import TripDAO


class TripService:
    def get_trips_by_user(self, user):
        trips = []
        is_friend = False
        logged_user = self.get_logged_user()
        if logged_user:
            for friend in user.friends:
                if friend == logged_user:
                    is_friend = True
                    break
            if is_friend:
                return TripDAO.find_trips_by_user(user)
            else:
                return trips
        else:
            raise UserNotLoggedIn()

    def get_logged_user(self):
        return session.get_logged_user()
