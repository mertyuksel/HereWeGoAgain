using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("genre")]
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Title { get; set; }

    }
}
