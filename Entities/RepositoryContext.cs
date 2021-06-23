using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviePerson>()
                .HasKey(bc => new { bc.MovieId, bc.PersonId });
           
            modelBuilder.Entity<MoviePerson>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.MoviePersons)
                .HasForeignKey(bc => bc.MovieId);
        
            modelBuilder.Entity<MoviePerson>()
                .HasOne(bc => bc.Person)
                .WithMany(c => c.MoviePersons)
                .HasForeignKey(bc => bc.PersonId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MoviePerson> MoviePersons { get; set; }
    }
}
