use crate::Trip;
use std::rc::Rc;

#[derive(Eq, PartialEq, Debug)]
pub struct User {
    name: String,
    friends: Vec<Rc<User>>,
    trips: Vec<Trip>,
}

impl User {
    pub fn new(name: &str) -> Self {
        Self {
            name: name.to_owned(),
            trips: vec![],
            friends: vec![],
        }
    }

    pub fn name(&self) -> &str {
        &self.name
    }

    pub fn add_friend(&mut self, friend: Rc<User>) {
        self.friends.push(friend);
    }

    pub fn friends(&self) -> &Vec<Rc<User>> {
        &self.friends
    }

    pub fn add_trip(&mut self, trip: Trip) {
        self.trips.push(trip);
    }

    pub fn is_friend_with(&self, other: &User) -> bool {
        self.friends.iter().any(|x| x.as_ref() == other)
    }
}
