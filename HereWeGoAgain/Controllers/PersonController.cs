using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HereWeGoAgain.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public PersonController(
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // api/person
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

        // api/person/id
        [HttpGet("{id}", Name = "PersonById")]  
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
       
        // api/person/id/details
        [HttpGet("{id}/details")]
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

        // api/person
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

                var createdPerson = _mapper.Map<PersonDto>(personEntity);

                return CreatedAtRoute("PersonById", new { id = createdPerson.PersonId }, createdPerson);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        // api/person/id
        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] PersonForUpdateDto person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest("Person object is null");
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

        // api/person/id
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

                if (_repository.Person.FindByCondition(t => t.MoviePersons.Any(t => t.PersonId == id)).Any())
                {
                    return BadRequest("Cannot delete person. It has related movies. Delete those movies first");
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
