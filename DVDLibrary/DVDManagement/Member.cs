using System;

namespace DVDLibrary
{
    public class Member
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
        private int MaxBorrows { get; set; }
        public DVDBorrowCount[] MovieBorrowHistory { get; set; } //store borrow history, never remove

        public Member(string firstName, string lastName, string phoneNumber, string pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.pin = pin;
            MaxBorrows = 5;
            currentBorrowing = new string[MaxBorrows];
        }
        public Member() { } //empty constructor

        public override string ToString()
        {
            return FirstName?.ToString() + " " + LastName?.ToString() + " " + PhoneNumber?.ToString();
        }
        public void AddToBorrow(string movieTitle) //add movie dvd to array of current borrowing 
        {
            try
            {
                if (currentBorrowing.Length >= MaxBorrows)
                {
                    throw new InvalidOperationException("Member cannot borrow more than 5 DVDs");                    
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
                AddBorrowHistory(movieTitle); //call AddBorrowHistory and bring in movieTitle
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return; // return to previous command
            }
        }
        public void RemoveFromBorrow(string movieTitle) //remove movieTitle from currentBorrowing array
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
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return; // return to previous command 
            }
        }

        public void AddBorrowHistory(string movieTitle) //store new borrw to array of borrowing history
        {            
            for(int index = 0; index< MovieBorrowHistory.Length && MovieBorrowHistory[index] != null; index++)
            {
                if (MovieBorrowHistory[index].DVDName == movieTitle)
                {
                    MovieBorrowHistory[index].Count++; //increment count
                    SortBorrowedHistory(index); //call SortBorrowedHistory
                    //return;
                }
            }
        }
        public void SortBorrowedHistory(int index)//use mergesort and display top 3 borrowed history.
        {
            DVDBorrowCount[] sortedArray = Mergesort<DVDBorrowCount>.Sort(MovieBorrowHistory); //call mergesort
            Console.WriteLine("Top 3 frequent borrowed movies are: ");
            for(int i=index; i>=Math.Max(0, index-2);i--)
            {
                Console.WriteLine($"{index - i + 1}.{sortedArray[i].DVDName}{sortedArray[i].Count} Times"); //print top 3 in descending order
            }
                        
        }
    }
}
