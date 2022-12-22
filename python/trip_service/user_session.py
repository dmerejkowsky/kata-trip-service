class _UserSession:
    def get_logged_user(self):
        raise Exception("Cannot use UserSession during unit tests")

    def is_user_logged_in(self, user):
        raise Exception("Cannot use UserSession during unit tests")


session = _UserSession()
