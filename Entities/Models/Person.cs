using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("person")]
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string From { get; set; }
        public ICollection<MoviePerson> MoviePersons { get; set; }
    }
}
