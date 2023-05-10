namespace DVDLibrary
{
    public static class PopulateData
    {
        public static List<Movie> GenerateMovies()
        {
            List<Movie> movies = new();

            // Define an array of movie titles
            string[] movieTitles = { "JavaScript", "TypeScript", "See Sharp", "See plus plus", "Python", "Ruby" };

            // Auto-populate 6 movie objects with random details
            for (int i = 0; i < movieTitles.Length; i++)
            {
                Movie newMovie = new(
                    title: movieTitles[i],
                    duration: new Random().Next(90, 181), // random duration between 90 and 180 minutes
                    genre: Enum.GetNames(typeof(Movie.MovieGenres))[new Random().Next(0, Enum.GetNames(typeof(Movie.MovieGenres)).Length)], // random genre
                    classification: Enum.GetNames(typeof(Movie.MovieClassifications))[new Random().Next(0, Enum.GetNames(typeof(Movie.MovieClassifications)).Length)], // random classification
                    numberOfDVDs: new Random().Next(1, 11) // random number of available DVDs between 1 and 10
                );
                movies.Add(newMovie);
            }
            return movies;
        }
        public static List<Member> GenerateMembers(int numMembers)
        {
            List<Member> members = new List<Member>();
            Random random = new Random();

            string[] firstNames = { "Alice", "Bob", "Charlie", "David", "Emily", "Frank", "Grace", "Henry", "Isabelle", "Jack" };
            string[] lastNames = { "Adams", "Brown", "Clark", "Davis", "Edwards", "Ford", "Garcia", "Harris", "Irwin", "Jones" };

            for (int i = 0; i < numMembers; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                string phoneNumber = $"555-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
                string pin = random.Next(1000, 9999).ToString();

                Member newMember = new Member(firstName, lastName, phoneNumber, pin);
                members.Add(newMember);
            }
            return members;
        }


    }
}
