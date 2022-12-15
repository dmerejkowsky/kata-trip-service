#![allow(dead_code)]

mod session;
mod trip;
mod user;

pub use session::Session;
pub use trip::Trip;
pub use user::User;

pub trait UserSession {
    fn get_logged_user(&self) -> Option<&User>;
}

pub trait Repository {
    fn get_trips_by_user(&self, user: &User) -> Vec<Trip>;
}

pub struct TripService {
    session: Box<dyn UserSession>,
    repository: Box<dyn Repository>,
}

struct Database;

impl Repository for Database {
    fn get_trips_by_user(&self, _user: &User) -> Vec<Trip> {
        panic!("Cannot use Database struct in unit tests");
    }
}

impl TripService {
    pub fn new(session: Box<dyn UserSession>, repository: Box<dyn Repository>) -> Self {
        Self {
            session,
            repository,
        }
    }

    pub fn get_trips_by_user(&self, user: &User) -> Result<Vec<Trip>, String> {
        let logged_user = self
            .session
            .get_logged_user()
            .ok_or_else(|| "Not logged in".to_owned())?;

        let trips = if user.is_friend_with(logged_user) {
            self.repository.get_trips_by_user(user)
        } else {
            vec![]
        };
        Ok(trips)
    }
}

#[cfg(test)]
mod tests;
