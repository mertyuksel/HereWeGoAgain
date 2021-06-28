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

// DONE: get all people 
// DONE: get one person by id 
// get person with details 
// create person 
// update person 
// delete person 

namespace HereWeGoAgain.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context;

        public PersonController(IRepositoryWrapper repository,
            IMapper mapper,
            RepositoryContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAllPeople()
        {
            try
            {
                var people = _repository.Person.GetAllPersons();

                // is null control necessary ? test it. 

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

            var personResult = _mapper.Map<PersonDto>(person);
            return Ok(personResult);
        }

        // TODO: ICollection field iceren modelleri DTO'ya map etmek. 
        [HttpGet("{id}/movie")]
        public IActionResult GetPersonWithDetails(Guid id)
        {
            try
            {
                var detailedPerson = _repository.Person.GetPersonWithDetails(id);

                if (detailedPerson == null)
                {
                    return NotFound();
                }

                var detailedPersonResult = _mapper.Map<PersonWithDetails>(detailedPerson);

                return Ok(detailedPersonResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

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

        // Testing 
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(Guid id)
        {
            try
            {
                var person = _repository.Person.GetPersonById(id);

                if (person == null)
                {
                    return NotFound();
                }

                // ?? 
                if (_repository.Person.FindByCondition(t => t.MoviePersons.Any(t => t.PersonId == id)).Any())
                {
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.Person.DeletePerson(person);
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
