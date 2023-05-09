using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary
{
    interface iMovie
    {
        string Title { get; set; }
        int Duration { get; set; }
        string Genre { get; set; }
        string Classification { get; set; }
        int NumberOfBorrows { get; set; }
        int NumberOfDVDs { get; set; }
        string ToString();
        iMemberCollection MembersBorrowing { get; } //list of members borrowing this movie
        void AddMembersBorrowing(iMember member);
        void RemoveMembersBorrowing(iMember member);

    }
}
