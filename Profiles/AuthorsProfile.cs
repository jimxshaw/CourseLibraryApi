using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Helpers;

namespace CourseLibrary.API.Profiles
{
  public class AuthorsProfile : Profile
  {
    public AuthorsProfile()
    {
      // AutoMapper will automatically map properties
      // with the same names. Special property mappings
      // will require additional configurations through projections.
      CreateMap<Entities.Author, Models.AuthorDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(destination => destination.Age, option => option.MapFrom(source => source.DateOfBirth.GetCurrentAge()));
    }
  }
}