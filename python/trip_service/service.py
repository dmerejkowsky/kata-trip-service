from trip_service.user_session import session
from trip_service.errors import UserNotLoggedIn


class TripService:
    def get_trips_by_user(self, user):
        trips = []
        is_friend = False
        logged_user = session.get_logged_user()
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
