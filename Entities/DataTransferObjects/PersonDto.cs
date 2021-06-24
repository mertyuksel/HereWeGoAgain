using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class PersonDto
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string From { get; set; }
        
        // public ICollection<MoviePerson> MoviePersons { get; set; }
    }
}
