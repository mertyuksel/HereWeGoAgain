using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataTransferObjects
{
    public class MovieForCreationDto
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
       
        // [ForeignKey(nameof(Genre))]   
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MoviePerson> MoviePersons { get; set; }
    }
}
