using System;

namespace DVDLibrary
{
    interface iMemberCollection
    {
        void Add(iMember member);
        void Remove(iMember member);
        void GetMemberDetails();
        void GetListOfBorrowers();
        bool Search(iMember member); //search a member by name
        iMember[] ListOfMembers(); //display a list of members 
    }
}
