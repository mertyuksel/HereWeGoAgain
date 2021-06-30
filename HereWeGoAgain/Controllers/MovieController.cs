using System;
using System.Collections.Generic;
using System.Linq;
using Entities.DataTransferObjects;
using Entities.Models;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HereWeGoAgain.Controllers
{
    [Route("api/movie")]
    [ApiController]  
    public class MovieController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public MovieController(
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // api/movie 
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            try
            {
                var movies = _repository.Movie.GetAllMovies();

                // null control necessary ?? 

                var moviesResult = _mapper.Map<IEnumerable<MovieDto>>(movies);
                return Ok(moviesResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // api/movie/id 
        [HttpGet("{id}")]
        public IActionResult GetMovieById(Guid id)
        {
            try
            {
                var movie = _repository.Movie.GetMovieById(id);

                if (movie == null)
                {
                    return NotFound();
                }

                var movieResult = _mapper.Map<MovieDto>(movie);
                return Ok(movieResult);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // api/movie/id/details 
        [HttpGet("{id}/details")]
        public IActionResult GetMovieByDetails(Guid id)
        {
            try
            {
                var movieWithDetail = _repository.Movie.GetMovieByDetails(id);

                if (movieWithDetail == null)
                {
                    return NotFound();
                }

                var movieWithDetailResult = _mapper.Map<MovieWithDetails>(movieWithDetail);

                return Ok(movieWithDetail);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // api/movie
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieForCreationDto movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var movieEntity = _mapper.Map<Movie>(movie);
                _repository.Movie.CreateMovie(movieEntity);
                _repository.Save();

                // yaratilmis olani goster burada hepsini degil CreatedAtRoute ile. 
                var movies = _repository.Movie.GetAllMovies();

                return Ok(movie);
            }
            catch (Exception ex)
            {

                string innerMessage = (ex.InnerException != null)
                      ? ex.InnerException.Message
                      : "";

                return StatusCode(500, "Internal Server Error" + ex.Message + innerMessage);

            }
        }

        // api/movie/id
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(Guid id, [FromBody] MovieForUpdateDto movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var movieEntity = _repository.Movie.GetMovieById(id);

                if (movieEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(movie, movieEntity);
                _repository.Movie.UpdateMovie(movieEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // api/movie/id 
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(Guid id)
        {
            try
            {
                var movie = _repository.Movie.GetMovieById(id);

                if (movie == null)
                {
                    return NotFound();
                }

                // better approach ??  
                if (_repository.Movie.FindByCondition(t => t.MoviePersons.Any(t => t.MovieId == id)).Any())
                {
                    return BadRequest("Cannot delete the movie. It has related people. Delete those people first");
                }

                _repository.Movie.DeleteMovie(movie);
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
