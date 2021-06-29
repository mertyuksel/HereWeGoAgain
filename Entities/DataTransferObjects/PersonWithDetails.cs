using Entities.Models;
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class PersonWithDetails
    {
        public string Name { get; set; }

        public ICollection<MoviePerson> MoviePersons;
    }
}
