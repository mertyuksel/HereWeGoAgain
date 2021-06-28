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
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {

        }
        public IEnumerable<Person> GetAllPersons()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        public Person GetPersonById(Guid personId)
        {
            return FindByCondition(person => person.PersonId.Equals(personId))
                .FirstOrDefault();
        }

        public Person GetPersonWithDetails(Guid personId)
        {
            return FindByCondition(t => t.MoviePersons.Any(t => t.PersonId == personId))
                    .Include(x => x.MoviePersons).ThenInclude(x => x.Movie).FirstOrDefault();
        }

        public void CreatePerson(Person person)
        {
            Create(person);
        }

        public void UpdatePerson(Person person)
        {
            Update(person);
        }

        public void DeletePerson(Person person)
        {
            Delete(person);
        }
    }
}
