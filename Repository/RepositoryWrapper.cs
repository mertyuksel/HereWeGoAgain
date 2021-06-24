using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPersonRepository _person;
        private IMovieRepository _movie;
        private IGenreRepository _genre;
        private IMoviePersonRepository _moviePerson;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IPersonRepository Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new PersonRepository(_repoContext);
                }

                return _person;
            }
        }

        public IMovieRepository Movie
        {
            get
            {
                if (_movie == null)
                {
                    _movie = new MovieRepository(_repoContext);
                }

                return _movie;
            }
        }

        public IGenreRepository Genre
        {
            get
            {
                if (_genre == null)
                {
                    _genre = new GenreRepository(_repoContext);
                }

                return _genre;
            }
        }

        public IMoviePersonRepository MoviePerson
        {
            get
            {
                if (_moviePerson == null)
                {
                    _moviePerson = new MoviePersonRepository(_repoContext);
                }

                return _moviePerson;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
