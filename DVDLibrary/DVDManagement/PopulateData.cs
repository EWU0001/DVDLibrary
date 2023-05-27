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
    }
}
