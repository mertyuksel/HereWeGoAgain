
namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPersonRepository Person { get; }
        IMovieRepository Movie { get; }
        IGenreRepository Genre { get; }
        IMoviePersonRepository MoviePerson { get; }
        void Save();
    }
}
