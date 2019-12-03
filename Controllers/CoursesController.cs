using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
  [ApiController]
  [Route("api/authors/{authorId}/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly ICourseLibraryRepository _repo;
    private readonly IMapper _mapper;

    public CoursesController(ICourseLibraryRepository courseLibraryRepository,
                              IMapper mapper)
    {
      _repo = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
  }
}