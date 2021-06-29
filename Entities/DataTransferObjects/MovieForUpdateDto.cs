﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class MovieForUpdateDto
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MoviePerson> MoviePersons { get; set; }
    }
}
