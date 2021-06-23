using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("movieperson")]
    public class MoviePerson
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }

    }
}
