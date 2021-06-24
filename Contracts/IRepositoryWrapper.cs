using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPersonRepository Person { get; }
        IMovieRepository Movie { get; }
        IGenreRepository Genre { get; }
        IMoviePersonRepository MoviePerson { get; }
    }
}
