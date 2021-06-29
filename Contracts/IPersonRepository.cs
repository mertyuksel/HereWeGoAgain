using Entities.Models;
using System;
using System.Collections.Generic;


namespace Contracts
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        public IEnumerable<Person> GetAllPersons();
        public Person GetPersonById(Guid personId);
        public Person GetPersonWithDetails(Guid personId);
        public void CreatePerson(Person person);
        public void UpdatePerson(Person person);
        public void DeletePerson(Person person);

    }
}
