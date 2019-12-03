using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
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

    [HttpGet()]
    public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
    {
      if (!_repo.AuthorExists(authorId))
      {
        return NotFound();
      }

      var coursesForAuthorFromRepo = _repo.GetCourses(authorId);

      return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesForAuthorFromRepo));
    }

    [HttpGet("{courseId}")]
    public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
    {
      if (!_repo.AuthorExists(authorId))
      {
        return NotFound();
      }

      var courseForAuthorFromRepo = _repo.GetCourse(authorId, courseId);

      if (courseForAuthorFromRepo == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
    }


  }
}