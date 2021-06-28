using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HereWeGoAgain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonForCreationDto, Person>();
            CreateMap<PersonForUpdateDto, Person>();
            CreateMap<Person, PersonWithDetails>().PreserveReferences(); // preserve references detaylarina bak. 
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieWithDetails>();
            CreateMap<MovieForCreationDto, Movie>();
        }
    }
}
