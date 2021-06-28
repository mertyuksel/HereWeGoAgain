using AutoMapper;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Entities.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace HereWeGoAgain.Controllers
{
    [Route("api/movie")]
    [ApiController] // tam olarak neye yariyor bi bak ezbere yaptin. 
    public class MovieController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly RepositoryContext _context; // delete this after testing process 

        public MovieController(
            IRepositoryWrapper repository,
            IMapper mapper,
            RepositoryContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

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

        [HttpGet("{id}/details")]
        public IActionResult GetMovieByDetails(Guid id)
        {
            try
            {
                // TODO: cannot get genre fix this. 
                var movieWithDetail = _repository.Movie.FindByCondition(t => t.MoviePersons.Any(t => t.MovieId == id))
                    .Include(x => x.MoviePersons).ThenInclude(x => x.Person).FirstOrDefault();

                var movieWithDetailResult = _mapper.Map<MovieWithDetails>(movieWithDetail);

                // TODO: Dto eksik ic ice indexleri hepsini map ediyor istediklerimi nasil map ederim bakicam.

                return Ok(movieWithDetail);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }



        // BU KISIM CALISMIYOR 
        // MODELI ALIYOR AMA SAVE KISMINDA PROBLEM YARATIYOR 
        // VERIYI EKSIK GONDERIYORUM BUYUK IHT.
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

                var movies = _repository.Movie.GetAllMovies();

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }
        }
    }
}
