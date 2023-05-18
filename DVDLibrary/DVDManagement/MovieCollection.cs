using System.Collections.Generic;

namespace DVDLibrary
{
    public class MovieCollection
    {
        private const int Size = 999; //assume max 1000 key,value paris
        private readonly Movie[] _hashTable = new Movie[Size];
        private readonly int[] _probes = new int[Size];
        private List<DVDBorrowCount> _borrowCounts = new List<DVDBorrowCount>();

        private static int Hashing(string title) //covert movie title to hash
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash += c;
            }
            return hash % Size;
        }

        public bool AddMovie(Movie movie) //add new movie object
        {
            if (SearchMovie(movie.Title) != null) //duplicate search here
            {
                Console.WriteLine("Movie already exists in collection");
                return false;
            }
            int hash = Hashing(movie.Title); //calculate hash code
            int i = 0;
            while (_hashTable[(hash + i * i) % Size] != null)
            {
                if (_hashTable[(hash + i * i) % Size].Title == movie.Title) // movie already exists in the collection
                {
                    return false;
                }
                i++;
                if (i == Size) // table is full
                {
                    throw new Exception("Table is full");
                }
            }
            if (i > 0) // collision has occurred
            {
                int j = 1;
                while (_hashTable[(hash + j * j) % Size] != null)
                {
                    j++;
                    if (j == Size) // table is full
                    {
                        throw new Exception("Table is full");
                    }
                }
                hash = (hash + j * j) % Size; // calculate new hash code using quadratic probing
            }
            _hashTable[hash] = movie;
            _probes[hash] = i + 1;
            Console.WriteLine($"Movie: {movie.Title} added to the system");
            return true;
        }

        public void AddDVD(string movieTitle, int inputNumberOfDVDs) //add dvd to current movie object
        {
            // Prompt the user to enter a movie title and read the input
            //Console.WriteLine("Enter movie title >> ");
            //string? inputTitle = Console.ReadLine();

            // Search for the movie in the hashtable
            Movie movie = SearchMovie(movieTitle!);

            if (movie != null)
            {
                // If the movie is found, prompt the user to enter the number of DVDs to be added
                //int inputNumberOfDVDs;
                do
                {
                    Console.WriteLine("Number of DVD to be added >> ");
                } while (!int.TryParse(Console.ReadLine(), out inputNumberOfDVDs));

                movie.NumberOfDVDs += inputNumberOfDVDs;
                Console.WriteLine($"{movie.Title} has {movie.NumberOfDVDs} DVD");
            }
            else
            {
                // If the movie is not found, prompt the user to enter the details and add it to the hashtable
                Console.WriteLine("Movie not found");
                return;
            }
        }

        public bool RemoveDVD(string title, int numberOfRemove) //deduct dvd from movie object
        {
            Movie movie = SearchMovie(title);
            if (movie != null)
            {
                int hash = Hashing(title);
                int i = 0;
                while (_hashTable[(hash + i * i) % Size] != null)
                {
                    if (_hashTable[(hash + i * i) % Size].Title == title)
                    {
                        _hashTable[(hash + i * i) % Size].NumberOfDVDs -= numberOfRemove;
                        if (_hashTable[(hash + i * i) % Size].NumberOfDVDs <= 0)
                        {
                            _hashTable[(hash + i * i) % Size] = null!;
                            _probes[(hash + i * i) % Size] = 0;
                        }
                        Console.WriteLine($"{numberOfRemove} DVDs removed from {title} sucessfully");
                        return true;
                    }
                    i++;
                }
            }
            Console.WriteLine("movie not found");
            return false;
        }

        public Movie SearchMovie(string title) //search movie in hashtable
        {
            int hash = Hashing(title);

            for (int i = 0; i < Size; i++)
            {
                if (_hashTable[(hash + i * i) % Size] != null &&
                    _hashTable[(hash + i * i) % Size].Title == title)
                {
                    return _hashTable[(hash + i * i) % Size];
                }
            }
            return null!;
        }
        public void GetMovieDetails() //display details of a single movie given by movie title
        {
            Console.WriteLine("Input Movie title to see details");
            string? inputTitle = Console.ReadLine();
            Movie movie = SearchMovie(inputTitle!);
            if (movie != null)
            {
                Console.WriteLine(movie);
            }
            else
            {
                Console.WriteLine("Movie not found");
            }
        }
        public void DisplayMovies()
        {
            // Get all movies from the hash table
            List<Movie> movies = _hashTable.Where(movie => movie != null).ToList();

            // pass the list of movies to sort method in Mergesort class
            Mergesort<Movie>.Sort(movies);

            // Display the sorted movies
            Console.WriteLine("Displaying Movies in dictionary order: ");
            foreach (Movie movie in movies)
            {
                Console.WriteLine(
                    $"Movie Title: {movie.Title} " +
                    $"Genre: {movie.Genre} " +
                    $"Classification: {movie.Classification}" +
                    $"Duration: {movie.Duration}" +
                    $"Number of DVD available: {movie.NumberOfDVDs}");
            }
        }

        public bool BorrowMovie(string movieTitle) //search movie and deduct movie number of DVD when user borrows
        {
            // Search for the movie in the hashtable
            Movie movie = SearchMovie(movieTitle);

            if (movie != null)
            {
                if (movie.NumberOfDVDs > 0)
                {
                    movie.NumberOfDVDs--;//deduct Movie.numberOfDVDs
                    movie.TimesBorrowed++; //increment TimesBorrowed
                    Console.WriteLine($"Movie exist and You borrowed {movie.Title}");
                    UpdateBorrowCount(movie.Title);
                    return true;
                }
                Console.WriteLine($"No DVD available for the movie {movie.Title}.");
            }
            return false; //if not found back to previous command
        }
        public bool ReturnMovie(string movieTitle)
        {
            // Search for the movie in the hashtable
            Movie movie = SearchMovie(movieTitle);

            if (movie != null)
            {
                if (movie.NumberOfDVDs >= 0)
                {
                    movie.NumberOfDVDs++;//increment Movie.numberOfDVDs
                    Console.WriteLine($"You borrowed {movie.Title}");
                    return true;
                }
                Console.WriteLine($"No DVD available for the movie {movie.Title}.");
                return false;
            }
            return false; //if not found back to previous command
        }
        private void UpdateBorrowCount(string movieTitle)
        {
            int index = -1;
            int emptyIndex = -1;

            for (int i = 0; i < _borrowCounts.Count; i++)
            {
                if (_borrowCounts[i] != null && _borrowCounts[i].DVDName == movieTitle)
                {
                    index = i;
                    break;
                }
                else if (_borrowCounts[i] == null && emptyIndex == -1)
                {
                    emptyIndex = i;
                }
            }
            Console.WriteLine($"Debug: index = {index}, emptyIndex = {emptyIndex}");

            if (index != -1)
            {
                _borrowCounts[index].Count++;
            }
            else if (emptyIndex != -1)
            {
                _borrowCounts[emptyIndex] = new DVDBorrowCount(movieTitle, 1);
            }
            else
            {
                _borrowCounts.Add(new DVDBorrowCount(movieTitle, 1));
            }
        }

        public void DisplayTopBorrowedMovies()
        {
            List<DVDBorrowCount> sortedBorrowCounts = Mergesort<DVDBorrowCount>.Sort(_borrowCounts);

            int count = 3; // Specify the desired count of top borrowed movies
            Console.WriteLine($"Displaying Top {count} Borrowed Movies:");

            for (int i = 0; i < Math.Min(sortedBorrowCounts.Count, count); i++)
            {
                DVDBorrowCount borrowCount = sortedBorrowCounts[i];
                Console.WriteLine($"{i + 1}. Movie Title: {borrowCount.DVDName}, Borrow Count: {borrowCount.Count}");
            }
        }
        //for performance analysis, number of probe require to find  movie, -1 if movie doesn't exist in collection
        // public int GetProbeCount(string title)   
        // {
        //     int hash = Hashing(title);
        //     int i = 0;
        //     while (_hashTable[(hash + i * i) % Size] != null && i < Size)
        //     {
        //         if (_hashTable[(hash + i * i) % Size].Title == title)
        //         {
        //             return _probes[(hash + i * i) % Size];
        //         }
        //         i++;
        //     }
        //     return -1;
        // }
    }
}