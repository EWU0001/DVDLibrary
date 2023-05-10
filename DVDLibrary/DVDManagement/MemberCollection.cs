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
            return null;
        }
        private Member? Search(Member member, Node root)
        {
            if (root != null)
            {
                if (member.CompareTo(root.Obj) == 0)
                {
                    Console.WriteLine("\n Found Member");
                    return member;
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
                Console.WriteLine("\nMember Not found, please register new member!");
            }
            return null;
        }
        public void AddMember(Member member) //add member allows accessed in other classes
        {
            if (root == null)
                root = new Node(member);
            else
                Insert(member, root);
        }
        private void Insert(Member member, Node pointer)
        {
            if (member.CompareTo(pointer.Obj) < 0)
            {
                if (pointer.Left == null)
                    pointer.Left = new Node(member);
                else
                    Insert(member, pointer.Left);
            }
            else
            {
                if (pointer.Right == null)
                    pointer.Right = new Node(member);
                else
                    Insert(member, pointer.Right);
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
                }
            }
        }
        public string? GetMemberNumber(Member member) //get phone number of a member
        {
            Member? foundMember = Search(member);
            if(foundMember!=null)
            {
                Console.WriteLine($"Phone number for {foundMember.FirstName} is: {foundMember.PhoneNumber}");
                return foundMember.PhoneNumber;
            }
            else
            {
                Console.WriteLine($"Member{member.FirstName} not found");
                return null;
            }
        }
        public void GetListOfBorrowers(string movieTitle)
        {
            Console.WriteLine($"List of members who borrowed '{movieTitle}':");

            TraverseInOrder(); // iterate through all members in the collection

            Console.WriteLine("\n\n");

            // iterate through all members in the collection again to print borrowed movies
            TraverseInOrderAndPrintBorrowedMovies(root!, movieTitle);
        }
        private void TraverseInOrderAndPrintBorrowedMovies(Node root, string movieTitle)
        {
            if (root != null)
            {
                TraverseInOrderAndPrintBorrowedMovies(root.Left!, movieTitle);
                Member member = (Member)root.Obj;
                if (member.CurrentBorrowing!.Contains(movieTitle))
                {
                    Console.WriteLine($"{member.FirstName}");
                }

                TraverseInOrderAndPrintBorrowedMovies(root.Right!, movieTitle);
            }
        }
        public void MemberBorrowDVD(string? firstName, string? lastName, string movieTitle)//specific member borrow a movieDVD
        {
            Member memberToBorrow = new(firstName, lastName, "", "");
            Member? foundMember = Search(memberToBorrow);
            if (foundMember != null)
            {
                foundMember.AddToBorrow(movieTitle);
                Console.WriteLine($"Member {firstName} borrowed {movieTitle} successfully!");
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} not found in the system.");
            }
        }

        public void MemberReturnDVD(string? firstName, string? lastName, string movieTitle)//return a movieDVD
        {
            Member memberToBorrow = new Member(firstName, lastName, "", "");
            Member? foundMember = Search(memberToBorrow);
            if (foundMember != null)
            {
                foundMember.RemoveFromBorrow(movieTitle);
                Console.WriteLine($"Member {firstName} borrowed {movieTitle} successfully!");
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

        public bool CheckMemberAuth(string firstName, string lastName, string pin)
        {
            Member memberToSearch = new(firstName, lastName, "", pin);
            Member? foundMember = Search(memberToSearch);
            if (foundMember != null)
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
                Console.Write(root.Obj.ToString() + "");
                InOrder(root.Right!);
            }
        }
    }

}

