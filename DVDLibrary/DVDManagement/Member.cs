using System;

namespace DVDLibrary
{
    public class Member: IComparable
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
        public DVDBorrowCount[]? MovieBorrowHistory { get; set; } //store borrow history, never remove

        public Member(string? firstName, string? lastName, string? phoneNumber, string? pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.pin = pin;
            MaxBorrows = 5;
            currentBorrowing = new string[MaxBorrows];
        }
        public Member(string? firstName,string? lastName) 
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = "";
            this.pin = "";
            MaxBorrows = 5;
            currentBorrowing = new string[MaxBorrows];
        }
        public Member() { }

        public override string ToString()
        {
            return FirstName?.ToString() + " " + LastName?.ToString() + " " + PhoneNumber?.ToString();
        }
        public int CompareTo(object? obj)
        {
            if (obj is not Member other)
            {
                throw new ArgumentException("Object to compare must be a Member.", nameof(obj));
            }
            return this.lastName!.CompareTo(other.lastName) is not 0
                ? this.lastName!.CompareTo(other.lastName)
                : this.firstName!.CompareTo(other.firstName);
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

        public void AddBorrowHistory(string? movieTitle) //store new borrw to array of borrowing history
        {            
            for(int index = 0; index< MovieBorrowHistory!.Length && MovieBorrowHistory[index] != null; index++)
            {
                if (MovieBorrowHistory[index].DVDName == movieTitle)
                {
                    MovieBorrowHistory[index].Count++; //increment count
                    //return;
                }
            }
        }
        public void SortBorrowedHistory(string firstName, string lastName)
        {
            Member memberToFind = new(firstName, lastName);
            //Find member using BST search function given first and last name
            MemberCollection memberCollection = new();
            Member? member = memberCollection.Search(memberToFind);
            if(member == null)
            {
                Console.WriteLine($"Member {firstName}{lastName} not found.");
                return;
            }
            DVDBorrowCount[]? historyArray = member.MovieBorrowHistory;

            // Sort the member's history array using mergesort
            DVDBorrowCount[] sortedArray = Mergesort<DVDBorrowCount>.Sort(historyArray!);
            // Display the top 3 frequent borrowed movies
            Console.WriteLine($"Top 3 frequent borrowed movies for {firstName} {lastName}:");
            for (int i = 0; i < Math.Min(sortedArray.Length, 3); i++)
            {
                Console.WriteLine($"{i + 1}.{sortedArray[i].DVDName} ({sortedArray[i].Count} times)");
            }
        }

    }
}
