using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class MoviePersonRepository : RepositoryBase<MoviePerson>, IMoviePersonRepository
    {
        public MoviePersonRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }

    }
}
