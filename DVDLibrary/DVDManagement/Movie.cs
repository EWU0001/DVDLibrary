using System;

namespace DVDLibrary
{
    public class Movie : IComparable<Movie>
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int NumberOfDVDs { get; set; }
        public int TimesBorrowed { get; set; }

        public Movie(string title, int duration, string genre, string classification, int numberOfDVDs)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
            Classification = classification;
            NumberOfDVDs = numberOfDVDs;
        }
        public override string ToString()
        {
            return $" Movie Title: '{Title}' " +
                   $" Duration(minutes): '{Duration}' " +
                   $" Classification: '{Classification}' " +
                   $" Genre: '{Genre}' " +
                   $" Number of availble DVD: '{NumberOfDVDs}'";
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
        public enum MovieClassifications
        {
            G,
            PG,
            M15,
            MA15
        }
        public int CompareTo(Movie? other)
        {
            return this.Title.CompareTo(other?.Title);
        }
    }
}
