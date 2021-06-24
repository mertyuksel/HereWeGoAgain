using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HereWeGoAgain.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private RepositoryContext _context;

        public PersonController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            // var m = _context.Persons.FirstOrDefault(s => s.Name == "Leonardo DiCaprio");
            // var movie = _context.MoviePersons.FirstOrDefault(s => s.Person.Name == "Leonardo DiCaprio");

            // var test = new PersonRepository(_context);
            // var movies = test.GetAllPersons();

            var _repository = new RepositoryWrapper(_context); // interface ile dene 
            
            // testing equals testing2
            var testing = _repository.Person.FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();

            var testing2 = _repository.Person.GetAllPersons();

            return Ok(testing2);
        }
    }
}
