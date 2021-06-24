using Contracts;
using Entities;
using Entities.Models;
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


    }
}
