use crate::Trip;

#[derive(Eq, PartialEq, Debug, Clone)]
pub struct User {
    name: String,
    friends: Vec<User>,
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

    pub fn add_friend(&mut self, friend: User) {
        self.friends.push(friend);
    }

    pub fn friends(&self) -> &Vec<User> {
        &self.friends
    }

    pub fn add_trip(&mut self, trip: Trip) {
        self.trips.push(trip);
    }

    pub fn is_friend_with(&self, other: &User) -> bool {
        self.friends.iter().any(|x| x == other)
    }
}
