using System;

namespace DVDLibrary
{
    public class Movie : IComparable<Movie>
    {
        private string title;
        private int duration;
        private string genre;
        private string classification;
        private int numberOfDVDs;

        public string Title { get => title; set => title = value; }
        public int Duration { get => duration; set => duration = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Classification { get => classification; set => classification = value; }
        public int NumberOfDVDs { get => numberOfDVDs; set => numberOfDVDs = value; } //if zero delete the movie object 

        // public Movie() { } //empty constructor
        public Movie(string title, int duration, string genre, string classification, int numberOfDVDs)
        {
            this.title = title;
            this.duration = duration;
            this.genre = genre;
            this.classification = classification;
            this.numberOfDVDs = numberOfDVDs;
        }
        public override string ToString()
        {
            return $"Movie Title: {Title} " +
                   $"Duration(minutes): {Duration} " +
                   $"Classification: {Classification} " +
                   $"Genre: {Genre} " +
                   $"Number of availble DVD: {NumberOfDVDs}";
        }
        public enum MovieGenres
        {
            Drama,
            Adventure,
            Family,
            Action,
            SciFi,
            Comedy,
            Animated,
            Thriller
        }
        //public static void DisplayMovieGenres()
        //{
        //    foreach (string genre in Enum.GetNames(typeof(MovieGenres)))
        //    {
        //        Console.WriteLine(genre);
        //    }
        //}
        public enum MovieClassifications
        {
            G,
            PG,
            M15,
            MA15
        }
        //public static void DisplayMovieClassifications()
        //{
        //    foreach (string classification in Enum.GetNames(typeof(MovieClassifications)))
        //    {
        //        Console.WriteLine(classification);
        //    }
        //}
        public int CompareTo(Movie? other)
        {
            return this.Title.CompareTo(other?.Title);
        }
    }
}
