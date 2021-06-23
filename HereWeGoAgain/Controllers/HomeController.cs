using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HereWeGoAgain.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private RepositoryContext _context;
    
        public HomeController(RepositoryContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {


            // var m = _context.Persons.FirstOrDefault(s => s.Name == "Leonardo DiCaprio");
            var movie = _context.MoviePersons.FirstOrDefault(s => s.Person.Name == "Leonardo DiCaprio");



            return Ok(movie);
        }
    }
}
