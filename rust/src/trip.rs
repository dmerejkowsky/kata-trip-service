#[derive(Debug, Eq, PartialEq, Clone)]
pub struct Trip {
    title: String,
}

impl Trip {
    pub fn new(title: &str) -> Self {
        Self {
            title: title.to_owned(),
        }
    }
    pub fn title(&self) -> &str {
        &self.title
    }
}
