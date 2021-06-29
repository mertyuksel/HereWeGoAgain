using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("genre")]
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Title { get; set; }

    }
}
