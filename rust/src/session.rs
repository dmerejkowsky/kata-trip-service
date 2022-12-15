use crate::User;

pub struct Session;

impl Session {
    pub fn get_logged_user() -> Option<User> {
        panic!("Don't call Session during unit tests")
    }
}
