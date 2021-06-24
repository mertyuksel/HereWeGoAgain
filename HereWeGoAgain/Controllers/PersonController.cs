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
        private RepositoryContext _context;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PersonController(RepositoryContext context,
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
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

                // Q: 2 fielddan olusan dto gonderdigimde geri kalanlar null mi oluyor
                // yoksa degismeden kaliyor mu ? 

                // Dto da olmayan field degistirdiginde, o field'i map etmiyor
                // yano db degismiyor. 
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
