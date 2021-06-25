using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
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
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private RepositoryContext _context;

        public PersonController(IRepositoryWrapper repository,
            IMapper mapper,
            RepositoryContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;

        }


        [HttpGet("test")]
        public IActionResult JustTesting()
        {
            // _context.Movies:
            // ToList() = many
            // Single()
            // Where()  = many 

            // LINQ Query Syntax:
            // LINQ Method Syntax:

            // 1 
            // normal modelden hepsini okumak. 
            // var movies = _context.Movies.ToList();

            // 2 
            // single entity dondurur
            // var movies = _context.Movies.Single(m => m.Title == "BadTrip");

            // 3 
            // kosullu veri almak
            // var movies = _context.Movies.Where(m => m.Title.Contains("a")).ToList();

            // 4
            //var people = _repository.Person.FindByCondition(t => t.Role == "actor");

            //var peopleTest = _context.Persons.Select(p => p.Role == "actor").ToList();



            // 5
            //    var testing = _context.Persons
            //        .Select( p => new { eg = p.Role });
            /*
              OUTPUT: 
                0	
                eg:	"Uber Driver"
                1	
                eg:	"Actor"
                2	
                eg:	"Actor"
                3	
                eg:	"Actor"             
             
            */
            /*
            var testObject = _context.Persons.Where(p => p.Name.Contains("Leo")).Any(c => c.)
                .Include(mp => mp.MoviePersons);
            */


            /*
            var testObject = from person in _context.Persons
                             where person.Role == "Actor"
                             select person.PersonId;

            var secondTestObject = _context.Persons
                .Where(p => p.Role == "Uber Driver")
                .Select(s => new { s.Role, s.PersonId, s.From });

            var thirdTestObject = _context.Persons
                .Where(p => p.Name.Contains("Leo"))
                .Select(s => new { s.Name});
            */


            var justATest = _context.Persons
                .Where(p => p.Name.Contains("Leo"))
                .Select(s => s.MoviePersons);
                


            return Ok(justATest);
        }

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            try
            {
                var people = _repository.Person.GetAllPersons();

                var peopleResult = _mapper.Map<IEnumerable<PersonDto>>(people);

                return Ok(peopleResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]  // [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetPersonById(Guid id)
        {
            var person = _repository.Person.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet("{id}/movie")]
        public IActionResult GetPersonWithDetails(Guid id)
        {
            var person = _repository.Movie.FindByCondition(t => t.MoviePersons.Any(mp => mp.PersonId == id))
                 .Include(t => t.MoviePersons)
                 .Select(t => new { 
                 t.Title,
                 t.Year
                 })
                 .ToList();
           
            if (person == null)
            {
                return NotFound();
            }

            //var movies = person.MoviePersons.Select(t => t.Movie.Title).ToList();

            // TODO: GET MOVIES BY PERSON ID

            return Ok(person);
        }

        // NOT COMPLETE 
        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonForCreationDto person)
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

                var personEntity = _mapper.Map<Person>(person);

                _repository.Person.CreatePerson(personEntity);   
                _repository.Save();

                var people = _repository.Person.GetAllPersons();

                return Ok(people);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] PersonForUpdateDto person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var personEntity = _repository.Person.GetPersonById(id);
                if (personEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(person, personEntity);

                _repository.Person.UpdatePerson(personEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
