using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibrary
{
    interface iMovieCollection
    {
        void Add(iMovie movie);
        void Remove(iMovie movie);
        bool Search(iMovie movie);
        iMovie[] ListOfMovies(); //display list of movies
    }
}
