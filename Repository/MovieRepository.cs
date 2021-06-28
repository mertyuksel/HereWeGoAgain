using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    { 
        public MovieRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public IEnumerable<Movie> GetAllMovies()
        {
            return FindAll().OrderBy(m => m.Title).ToList();
        }

        public Movie GetMovieById(Guid movieId)
        {
            return FindByCondition(m => m.MovieId.Equals(movieId)).FirstOrDefault();
        }

        public Movie GetMovieByDetails(Guid movieId)
        {
            // ?? genre person gibi fieldlarin bos gelmesi -- bunun bir kolayi yok mu. 
            /*
            return FindByCondition(t => t.MoviePersons.Any(t => t.MovieId == movieId))
                    .Include(x => x.MoviePersons).ThenInclude(x => x.Movie).FirstOrDefault();
            */
           
            return FindByCondition(t => t.MoviePersons.Any(t => t.MovieId == movieId))
            .Include(x => x.MoviePersons).ThenInclude(x => x.Movie).FirstOrDefault();
        }

        public void CreateMovie(Movie movie)
        {
            Create(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            Update(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            Delete(movie);
        }
    }
}
