use crate::*;
use std::{collections::HashMap, rc::Rc};

#[test]
fn test_can_add_friends_to_user() {
    let mut bob = User::new("Bob");
    let charlie = Rc::new(User::new("Charlie"));
    bob.add_friend(Rc::clone(&charlie));

    let friends = bob.friends();
    assert_eq!(friends, &vec![Rc::clone(&charlie)]);

    assert!(bob.is_friend_with(&charlie));
}

struct RejectingSession;

impl UserSession for RejectingSession {
    fn get_logged_user(&self) -> Option<&User> {
        None
    }
}

struct AcceptingSession {
    user: Rc<User>,
}

impl AcceptingSession {
    fn new(user: Rc<User>) -> Self {
        Self { user }
    }
}

impl UserSession for AcceptingSession {
    fn get_logged_user(&self) -> Option<&User> {
        Some(&self.user)
    }
}

#[derive(Debug, Default)]
struct FakeDatabase {
    trips: HashMap<String, Vec<Trip>>,
}

impl FakeDatabase {
    fn new() -> Self {
        Default::default()
    }

    fn insert_trip(&mut self, name: &str, trip: Trip) {
        self.trips
            .entry(name.to_owned())
            .and_modify(|x| x.push(trip.clone()))
            .or_insert_with(|| vec![trip]);
    }
}

impl Repository for FakeDatabase {
    fn get_trips_by_user(&self, user: &User) -> Vec<Trip> {
        match self.trips.get(user.name()) {
            Some(trips) => trips.clone(),
            None => vec![],
        }
    }
}

#[test]
fn test_trip_service_return_error_if_not_logged_in() {
    let bob = User::new("Bob");
    let session = RejectingSession;
    let db = FakeDatabase::new();
    let service = TripService::new(Box::new(session), Box::new(db));
    let trips = service.get_trips_by_user(&bob);
    assert!(trips.is_err());
}

#[test]
fn test_trip_service_return_empty_if_not_friend() {
    let bob = Rc::new(User::new("Bob"));
    let session = AcceptingSession::new(Rc::clone(&bob));
    let db = FakeDatabase::new();
    let service = TripService::new(Box::new(session), Box::new(db));

    let trips = service.get_trips_by_user(&bob).unwrap();

    assert!(trips.is_empty());
}

#[test]
fn test_return_friends_trips() {
    let bob = Rc::new(User::new("Bob"));
    let mut charlie = User::new("Charlie");
    charlie.add_friend(Rc::clone(&bob));
    let to_peru = Trip::new("To Peru");
    let to_egypt = Trip::new("To Egypt");

    let session = AcceptingSession::new(Rc::clone(&bob));
    let mut db = FakeDatabase::new();
    db.insert_trip(charlie.name(), to_peru.clone());
    db.insert_trip(charlie.name(), to_egypt.clone());
    let service = TripService::new(Box::new(session), Box::new(db));

    let trips = service.get_trips_by_user(&charlie).unwrap();
    assert_eq!(trips, [to_peru, to_egypt]);
}
