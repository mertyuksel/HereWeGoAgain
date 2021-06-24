using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        public IEnumerable<Person> GetAllPersons();
        public Person GetOwnerById(Guid personId);

    }
}
