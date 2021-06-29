using System;

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
