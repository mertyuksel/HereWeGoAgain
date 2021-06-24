using Contracts;
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
        private IRepositoryWrapper _repository;


        public PersonController(RepositoryContext context, IRepositoryWrapper repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            // var m = _context.Persons.FirstOrDefault(s => s.Name == "Leonardo DiCaprio");
            // var movie = _context.MoviePersons.FirstOrDefault(s => s.Person.Name == "Leonardo DiCaprio");

            // var test = new PersonRepository(_context);
            // var movies = test.GetAllPersons();


            /* 
            var _repository = new RepositoryWrapper(_context); // interface ile dene 
            
           // testing equals testing2
            var testing = _repository.Person.FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();

            var testing2 = _repository.Person.GetAllPersons();
            */


            var ahshitherewegoagain  = _repository.Person.GetAllPersons();

            return Ok(ahshitherewegoagain);
        }


        [HttpGet("{id}")]  // [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetPersonById(Guid id)
        {
            var owner = _repository.Person.GetOwnerById(id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }


        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest("person object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("invalid model object");
                }

                _repository.Person.Create(person);
                _repository.Save();

                var ahshitherewegoagain = _repository.Person.GetAllPersons();

                return Ok(ahshitherewegoagain);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }



        }




    }
}
