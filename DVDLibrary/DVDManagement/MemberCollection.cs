using System;

namespace DVDLibrary
{
    public class MemberCollection
    {
        public List<Member> members;

        public MemberCollection()
        {
            members = new List<Member>();
        }
        public bool IsEmpty()
        {
            return members.Count == 0;
        }
        public Member Search(Member member) //search function allows access in other classes
        {
            int left = 0;
            int right = members.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (string.Compare(members[mid].FullName, member.FullName) == 0)
                    return members[mid];

                if (string.Compare(members[mid].FullName, member.FullName) < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return null!; // Member not found
        }
        public void AddMember(Member member) //add member allows accessed in other classes
        {
            Member? foundMember = Search(member);

            if (foundMember != null)
            {
                // Member already exists in the collection
                Console.WriteLine($"Member {member.FirstName} already exists in the collection");
            }
            else
            {
                // Insert the member at the appropriate position to maintain the sorted order
                int index = FindInsertionIndex(member);
                members.Insert(index, member);
                Console.WriteLine($"Member {member.FirstName} added");
            }
        }
        private int CompareMembers(Member member1, Member member2)
        {
            return string.Compare(member1.FullName, member2.FullName);
        }
        private int FindInsertionIndex(Member member)
        {
            int left = 0;
            int right = members.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                int compareResult = CompareMembers(members[mid], member);
                if (compareResult < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return left;
        }
        public void Remove(Member member) //delete a registered member from system
        {
            Member? foundMember = Search(member);
            if (foundMember != null)
            {
                members.Remove(foundMember);
                Console.WriteLine($"Member {member.FirstName} removed");
            }
            else
            {
                Console.WriteLine($"Member {member.FirstName} does not exist in the system");
            }

        }
        public string GetMemberNumber(Member member)
        {
            Member foundMember = Search(member);

            if (foundMember != null)
            {
                Console.WriteLine($"Phone number for {foundMember.FirstName} is: {foundMember.PhoneNumber}");
                return foundMember.PhoneNumber!;
            }
            else
            {
                Console.WriteLine($"Member {member.FirstName} not found");
                return null!;
            }
        }
        public void GetListOfBorrowers(string movieTitle) //list of members borrow a specific movie given by movie title.
        {
            Console.WriteLine($"List of members who borrowed '{movieTitle}':");

            bool hasBorrowers = PrintMembers(movieTitle);
            if (!hasBorrowers)
            {
                Console.WriteLine($"No one is borrowing the movie: '{movieTitle}'");
            }
        }
        private bool PrintMembers(string movieTitle)
        {
            bool hasBorrowers = false;

            foreach (Member member in members)
            {
                if (member.CurrentBorrowing != null && member.CurrentBorrowing.Contains(movieTitle))
                {
                    Console.WriteLine($"Member: {member.FullName}");
                    hasBorrowers = true;
                }
            }
            return hasBorrowers;
        }
        public void MemberBorrowDVD(string? firstName, string? lastName, string movieTitle, MovieCollection movieCollection)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("Invalid input: first name or last name is missing");
                return;
            }
            // Search for the member in the BST
            Member memberToBorrow = new(firstName, lastName, null, null);
            Member? foundMember = Search(memberToBorrow);

            if (foundMember == null)
            {
                Console.WriteLine($"Member {firstName} {lastName} not found in the system.");
                return;
            }
            if (foundMember.CurrentBorrowing!.Contains(movieTitle))
            {
                Console.WriteLine($"Already borrowed {movieTitle}");
                return;
            }
            //check if movie is available
            // Add the movie to member's borrow list
            try
            {
                foundMember.AddToBorrow(movieTitle);
                if (!movieCollection.BorrowMovie(movieTitle))
                {
                    foundMember.RemoveFromBorrow(movieTitle);
                    Console.WriteLine($"No DVD available for the movie {movieTitle}.");
                    return;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        public bool MemberReturnDVD(string? firstName, string? lastName, string movieTitle)//return a movieDVD
        {
            Member memberToBorrow = new Member(firstName, lastName, "", "");
            Member? foundMember = Search(memberToBorrow);
            if (foundMember != null)
            {
                if (foundMember.RemoveFromBorrow(movieTitle))
                {

                    return true;
                }

                else
                {
                    Console.WriteLine($"Movie {movieTitle} not found in the member's current borrowing.");
                }
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} not found in the system.");
            }
            return false;
        }
        public void GetBorrowedDVDsForMember(string firstName, string lastName)
        {
            Member? foundMember = Search(new Member(firstName, lastName, "", ""));

            if (foundMember is not null)
            {
                if (foundMember.CurrentBorrowing == null || foundMember.CurrentBorrowing.Length == 0)
                {
                    Console.WriteLine($"Member {firstName} {lastName} currently is not renting any DVDs");
                }
                else
                {
                    Console.WriteLine($"Member {firstName} {lastName} is currently borrowing DVDs of:");
                    foreach (var item in foundMember.CurrentBorrowing)
                    {
                        Console.WriteLine($"- {item}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} not found.");
            }
        }

        public bool CheckMemberAuth(string? firstName, string? lastName, string? pin)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(pin))
            {
                Console.WriteLine("Please provide all member details");
                return false;
            }

            Member memberToSearch = new(firstName, lastName, null, pin);
            Member? foundMember = Search(memberToSearch);
            if (foundMember != null && foundMember.Pin == pin)
            {
                Console.WriteLine("Member details are matched");
                return true;
            }

            Console.WriteLine("Check member details again");
            return false;
        }
    }
}

