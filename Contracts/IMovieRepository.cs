using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        public IEnumerable<Movie> GetAllMovies();
        public Movie GetMovieById(Guid movieId);
        public Movie GetMovieByDetails(Guid movieId);
        public void CreateMovie(Movie movie);
        public void UpdateMovie(Movie movie);
        public void DeleteMovie(Movie movie); 
    }
}
