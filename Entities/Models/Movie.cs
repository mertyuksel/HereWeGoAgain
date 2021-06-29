using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("movie")]
    public class Movie
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MoviePerson> MoviePersons { get; set; }
    }
}
