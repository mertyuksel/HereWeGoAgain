using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace HereWeGoAgain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto,Person>();
            CreateMap<PersonForCreationDto, Person>();
            CreateMap<PersonForUpdateDto, Person>();
            CreateMap<Person, PersonWithDetails>().PreserveReferences();  
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieWithDetails>();
            CreateMap<MovieForCreationDto, Movie>();
            CreateMap<MovieForUpdateDto, Movie>();
        }
    }
}
