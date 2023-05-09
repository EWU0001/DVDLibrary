using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary
{
    interface iMember
    {
        string FirsName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        int Pin { get; set; }
        string[] GetBorrowedDVDs { get; } //get list of movies memeber is currently borrowing

        void addMovie(iMovie movie);
        void removeMovie(iMovie movie);
        string ToString(); //member info
    }
}
