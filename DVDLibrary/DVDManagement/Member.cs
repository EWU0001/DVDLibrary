using System;

namespace DVDLibrary
{
    public class Member : IComparable
    {
        private string[] currentBorrowing; //array to store current DVDs for add & remove
        private string? firstName;
        private string? lastName;
        private string? phoneNumber;
        private string? pin;

        public string? FirstName { get => firstName; set => firstName = value; }
        public string? LastName { get => lastName; set => lastName = value; }
        public string? PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string? Pin { get => pin; set => pin = value; }
        public string[]? CurrentBorrowing { get => currentBorrowing; set => currentBorrowing = value!; }
        public int MaxBorrows { get; set; }
        public string FullName => $"{FirstName}{LastName}";

        public Member(string? firstName, string? lastName, string? phoneNumber, string? pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.pin = pin;
            MaxBorrows = 5;
            currentBorrowing = new string[MaxBorrows];
        }
        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public void AddToBorrow(string movieTitle) //add movie dvd to array of current borrowing 
        {
            try
            {
                if (currentBorrowing.Count(x => x != null) >= MaxBorrows)
                {
                    throw new InvalidOperationException("Member cannot borrow more than 5 DVDs");
                }
                if (currentBorrowing.Contains(movieTitle))
                {
                    Console.WriteLine($"member has already borrowed {movieTitle}");
                    return;
                }
                int index = 0;
                for (; index < currentBorrowing.Length; index++) //loop through currentBorrowing array
                {
                    if (currentBorrowing[index] == null)
                    {
                        break;
                    }
                }
                currentBorrowing[index] = movieTitle; //add movieTitle to currentBorrowing array
                Console.WriteLine($"Borrowed {movieTitle} successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return; // return to previous command
            }
        }
        public bool RemoveFromBorrow(string movieTitle) //remove movieTitle from currentBorrowing array
        {
            try
            {
                int index = -1; //find index of the movie in currentBorrowing array
                for (int i = 0; i < currentBorrowing.Length; i++)
                {
                    if (currentBorrowing[i] != null && currentBorrowing[i] == movieTitle)
                    {
                        index = i;
                        break;
                    }
                }
                if (index == -1) //shift element in borrwedDVDs array to left
                {
                    throw new InvalidOperationException("The movie has not been borrowed.");
                }
                for (int i = index; i < currentBorrowing.Length - 1; i++)
                {
                    currentBorrowing[i] = currentBorrowing[i + 1];
                }
                currentBorrowing[currentBorrowing.Length - 1] = null!; //set the last element in borrwedDVDs array to null                
                Console.WriteLine($"Movie {movieTitle} removed from member's account!");
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false; // return to previous command 
            }
        }
    }
}
