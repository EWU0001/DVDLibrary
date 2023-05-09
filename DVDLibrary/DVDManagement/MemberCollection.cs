using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DVDLibrary
{
    public class MemberCollection
    {
        //use list to store and manipulate members
        private List<Member?> members;

        public MemberCollection()
        {
            members = new List<Member?>();
        }
        public List<Member?> Members
        {
            get { return members; }
        }
        public Member? Search(string? firstName, string? lastName) //sequential search on members array
        {
            // Search for the member by first and last name
            foreach (Member? member in members)
            {
                if (member?.FirstName == firstName && member?.LastName == lastName)
                {
                    return member;
                }
            }
            Console.WriteLine("Member not found.");
            return null;
        }
        public void AddMember() //manually add members for testin purpose
        {
            Console.WriteLine("Input first name");
            string? inputFirstName = Console.ReadLine();
            Console.WriteLine("Input last name");
            string? inputLastName = Console.ReadLine();
            Console.WriteLine("Input phone number");
            string? inputPhoneNumber = Console.ReadLine();

            Console.WriteLine("Input 4-digit password pin:");
            string inputPin;
            while (true)
            {
                inputPin = Console.ReadLine();
                if (inputPin.Length != 4 || !int.TryParse(inputPin, out _) || !Regex.IsMatch(inputPin, @"^\d{4}$"))
                {
                    Console.WriteLine("Invalid PIN. Please enter a 4-digit number:");
                    continue;
                }
                break;
            }


            if (Members.Any(member => member?.FirstName == inputFirstName && member?.LastName == inputLastName))
            {
                Console.WriteLine("Member already exists in system.");
                return;
            }
            if (Search(inputFirstName, inputLastName) != null)
            {
                Console.WriteLine("Member already exists in system.");
                return;
            }
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i] == null)
                {
                    Members[i] = new Member(inputFirstName!, inputLastName!, inputPhoneNumber!, inputPin!);
                    Console.WriteLine("Member added! ");
                    return;
                }
            }
        }
        public void Add(string firstName, string lastName, string phoneNumber, string pin)//add members for auto-generate
        {
            // Check if the member already exists
            if (Search(firstName, lastName) != null)
            {
                Console.WriteLine("Member already exists.");
                return;
            }

            // Find the first empty slot in the members array
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i] == null)
                {
                    members[i] = new Member(firstName, lastName, phoneNumber, pin);
                    Console.WriteLine("Member added successfully!");
                    return;
                }
            }
            Console.WriteLine("Sorry, the member collection is full.");
        }
        public void Remove(string? firstName, string? lastName) //delete a registered member from the array
        {
            Member? memberToRemove = Search(firstName, lastName); //search through the members list
            if (memberToRemove != null)
            {
                for (int i = 0; i < members.Count; i++) // Find the member to remove
                {
                    if (members[i] == memberToRemove)
                    {
                        members[i] = null; // Remove the member by setting the corresponding array element to null
                        Console.WriteLine("Member removed!");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Member not found!");
            }
        }
        public void GetMemberNumber(string firstName, string lastName) //get phone number of a member
        {
            Member? memberToDisplay = Search(firstName, lastName);

            if (memberToDisplay != null)
            {
                Console.WriteLine("Member details:" + memberToDisplay.ToString());
            }
            else
            {
                Console.WriteLine("Member not found.");
                return;
            }
        }
        public void GetListOfBorrowers() //lookup movie and show all borrowing members names
        {
            Console.WriteLine("To find list of borrowers, input movie title here >> ");
            string? inputMovieTitle = Console.ReadLine();

            bool found = false;
            foreach (Member? member in members)
            {
                if (member?.CurrentBorrowing != null)
                {
                    foreach (string? currentBorrowing in member.CurrentBorrowing)
                    {
                        if (currentBorrowing == inputMovieTitle)
                        {
                            Console.WriteLine($"Member {member.FirstName} {member.LastName} is currently renting the movie '{inputMovieTitle}'.");
                            found = true;
                            break;
                        }
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"No one is renting the movie '{inputMovieTitle}'.");
            }
        }
        public void MemberBorrowDVD(string? firstName, string? lastName, string movieTitle)//specific member borrow a movieDVD
        {
            foreach (Member? member in members)
            {
                if (member?.FirstName == firstName && member?.LastName == lastName)
                {
                    member?.AddToBorrow(movieTitle);
                    Console.WriteLine($"Member {firstName} borrowed {movieTitle} successfully!");
                    break;
                }
            }
        }
        public void MemberReturnDVD(string? firstName, string? lastName, string movieTitle)//return a movieDVD
        {
            bool foundMember = false;
            foreach (Member? member in members)
            {
                if (member?.FirstName == firstName && member?.LastName == lastName)
                {
                    member?.RemoveFromBorrow(movieTitle);
                    Console.WriteLine($"Member {firstName} return {movieTitle} successfully!");
                    foundMember = true;
                    break;
                }
            }
            if (!foundMember)
            {
                Console.WriteLine($"Member {firstName} {lastName} not found.");
            }
        }
        public void GetBorrowedDVDsForMember(string firstName, string lastName)//lookup list of DVD under a member
        {
            bool foundMember = false;
            foreach (Member? member in members)
            {
                if (member?.FirstName == firstName && member?.LastName == lastName)
                {
                    if (member.CurrentBorrowing == null || member.CurrentBorrowing.Length == 0)
                    {
                        Console.WriteLine($"Member {firstName}{lastName} currently is not renting any DVDs");
                    }
                    else
                    {
                        Console.WriteLine($"Member {firstName}{lastName} is current borrowing DVDs of");
                        foreach (var item in member.CurrentBorrowing)
                        {
                            Console.WriteLine($"- {item}");
                        }
                        foundMember = true;
                        break;
                    }
                }
            }
            if (!foundMember)
            {
                Console.WriteLine($"Member {firstName} {lastName} not found.");
            }
        }
        public bool CheckMemberAuth(string firstName, string lastName, string pin)
        {
            foreach (Member? member in members)
            {
                if (member?.FirstName == firstName && member?.LastName == lastName && member?.Pin == pin)
                {
                    return true;
                }
            }
            Console.WriteLine("Check member details again");
            return false;

        }

    }
}
