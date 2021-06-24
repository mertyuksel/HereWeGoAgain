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

        // DONE
        public IEnumerable<Person> GetAllPersons()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        // DONE
        public Person GetPersonById(Guid personId)
        {
            return FindByCondition(person => person.PersonId.Equals(personId))
                .FirstOrDefault();
        }

        // NOT COMPLETE
        // HER KISININ YER ALDIGI FILMLERI DE PERSON DATASI ILE BERABER GETIRICEK 
        public Person GetOwnerWithDetails(Guid personId)
        {
            // placeholder
            return FindByCondition(person => person.PersonId.Equals(personId))
                .FirstOrDefault();
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
