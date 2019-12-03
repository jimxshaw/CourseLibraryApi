using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CourseLibrary.API.Profiles
{
  public class CoursesProfile : Profile
  {
    public CoursesProfile()
    {
      CreateMap<Entities.Course, Models.CourseDto>();
    }
  }
}