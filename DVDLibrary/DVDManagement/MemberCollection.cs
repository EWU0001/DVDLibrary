using System;

namespace DVDLibrary
{
    public class MemberCollection : IBSTree<Member>
    {
        public Node? root;

        public MemberCollection()
        {
            root = null;
        }
        public bool IsEmpty()
        {
            return root == null;
            throw new Exception("Is empty");
        }
        public Member? Search(Member member) //search function allows access in other classes
        {
            if (root is not null)
            {
                return Search(member, root);
            }
            Console.WriteLine("\nMember not found, register new member");
            return null;
        }
        private Member? Search(Member member, Node root)
        {
            if (root != null)
            {
                if (member.CompareTo(root.Obj) == 0)
                {
                    Console.WriteLine("\n Found Member");
                    return (Member)root.Obj;
                }
                else if (member.CompareTo(root.Obj) < 0 && root.Left is not null)
                {
                    Console.WriteLine("\n continue searching....");
                    return Search(member, root.Left);
                }
                else if (root.Right is not null)
                {
                    Console.WriteLine("\n continue searching....");
                    return Search(member, root.Right);
                }
            }
            Console.WriteLine("\nMember Not found, register new member!");
            return null;
        }
        public void AddMember(Member member) //add member allows accessed in other classes
        {
            if (root == null)
            {
                root = new Node(member);
                Console.WriteLine($"Member {member.FirstName} added successfully at node");
            }
            else
            {
                if (Insert(member, root))
                {
                    Console.WriteLine($"Member {member.FirstName} added successfully");
                }
                else
                {
                    Console.WriteLine($"Member {member.FirstName} already exists");
                }
            }
        }
        private bool Insert(Member member, Node pointer)
        {
            if (member.CompareTo(pointer.Obj) == 0)
            {
                return false;
            }
            else if (member.CompareTo(pointer.Obj) < 0)
            {
                if (pointer.Left == null)
                {
                    pointer.Left = new Node(member);
                    Console.WriteLine($"Member {member.FirstName} added successfully at left child");
                    return true;
                }
                else
                {
                    return Insert(member, pointer.Left);
                }
            }
            else
            {
                if (pointer.Right == null)
                {
                    pointer.Right = new Node(member);
                    Console.WriteLine($"Member {member.FirstName} added successfully at right child");
                    return true;
                }
                else
                {
                    return Insert(member, pointer.Right);
                }
            }
        }
        public void Remove(Member member) //delete a registered member from system
        {
            Node ptr = root!; //set pointer to root
            Node parent = null!; //parent of pointer
            while ((ptr != null) && member.CompareTo(ptr.Obj) != 0)
            {
                parent = ptr;
                if (member.CompareTo(ptr.Obj) < 0)
                    ptr = ptr.Left!;  // move to left child of pointer
                else
                    ptr = ptr.Right!; //else to right child of pointer
            }
            if (ptr != null) //if search found the object, two scenarios
            {
                if ((ptr.Left != null) && (ptr.Right != null)) //first scenario if object has two children
                {
                    if (ptr.Left.Right == null)// special case when right subtree of pointer left is empty
                    {
                        Console.WriteLine($"Member {member.FirstName} successfully removed from the system.");
                        ptr.Obj = ptr.Left.Obj;
                        ptr.Left = ptr.Left.Left;
                    }
                    else
                    {
                        Node p = ptr.Left;
                        Node pp = ptr;
                        while (p.Right != null)
                        {
                            pp = p;
                            p = p.Right;
                        }
                        Console.WriteLine($"Member {member.FirstName} successfully removed from the system.");
                        ptr.Obj = p.Obj;
                        pp.Right = p.Left;
                    }
                }
                else //when obj has one child or no child
                {
                    Node c = null!;
                    if (ptr.Left != null)
                        c = ptr.Left;
                    else if (ptr.Right != null)
                        c = ptr.Right;
                    if (ptr == root)
                        root = c;
                    else
                    {
                        if (ptr == parent.Left && parent.Left is not null)
                            parent.Left = c;
                        else if (parent.Right is not null)
                            parent.Right = c;
                    }
                    Console.WriteLine($"Member {member.FirstName} successfully removed from the system.");
                }
            }
            else
            {
                Console.WriteLine($"Member {member.FirstName} does not exist in the system");
            }
        }
        public string? GetMemberNumber(Member member) //get phone number of a member
        {
            Member? foundMember = Search(member);
            if (foundMember != null)
            {
                Console.WriteLine($"Phone number for {foundMember.FirstName} is: {foundMember.PhoneNumber}");
                return foundMember.PhoneNumber;
            }
            else
            {
                Console.WriteLine($"{member} not found ");
                return null;
            }
        }
        public void GetListOfBorrowers(string movieTitle) //list of members borrow a specific movie given by movie title.
        {
            Console.WriteLine($"List of members who borrowed '{movieTitle}':");

            bool hasBorrowers = false;
            TraverseInOrder(); // iterate through all members in the collection

            Console.WriteLine("\n\n");
            // iterate through all members in the collection to print borrowed movies
            hasBorrowers = PrintMembers(root!, movieTitle);
            if (!hasBorrowers)
            {
                Console.WriteLine($"No one is borrowing the movie:'{movieTitle}'");
            }
        }
        private bool PrintMembers(Node root, string movieTitle)
        {
            bool hasBorrowers = false;
            if (root != null)
            {
                hasBorrowers = PrintMembers(root.Left!, movieTitle);
                Member member = (Member)root.Obj;
                if (member.CurrentBorrowing != null && member.CurrentBorrowing!.Contains(movieTitle))
                {
                    // Console.WriteLine($"{member.FirstName}");
                    hasBorrowers = true;
                }
                PrintMembers(root.Right!, movieTitle);
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
            //check if movie is available
            if (!movieCollection.BorrowMovie(movieTitle))
            {
                Console.WriteLine($"No DVD available for the movie {movieTitle}.");
                return;
            }
            // Add the movie to member's borrow list
            try
            {
                foundMember.AddToBorrow(movieTitle);
                Console.WriteLine($"Member {firstName} {lastName} borrowed {movieTitle} successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                //if add to borrow fails, return the movie to collection
                movieCollection.ReturnMovie(movieTitle);
            }
        }
        public void MemberReturnDVD(string? firstName, string? lastName, string movieTitle)//return a movieDVD
        {
            Member memberToBorrow = new Member(firstName, lastName, "", "");
            Member? foundMember = Search(memberToBorrow);
            if (foundMember != null)
            {
                foundMember.RemoveFromBorrow(movieTitle);
                Console.WriteLine($"Member {firstName} returned {movieTitle} successfully!");
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} not found in the system.");
            }

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

        public void TraverseInOrder()
        {
            InOrder(root!);
        }
        private void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.Left!);
                Console.Write($"{((Member)root.Obj).FirstName}");
                InOrder(root.Right!);
            }
        }
        public void SortBorrowedHistory(string firstName, string lastName)
        {
            Member memberToFind = new(firstName, lastName, null, null);
            //Find member using BST search function given first and last name
            MemberCollection memberCollection = new();
            Member? member = memberCollection.Search(memberToFind);
            if (member == null)
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

