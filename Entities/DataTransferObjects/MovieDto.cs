using System;

namespace Entities.DataTransferObjects
{
    public class MovieDto
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
    }
}
