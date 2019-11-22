using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
  [ApiController]
  [Route("api/authors")]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _repo;

    public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
    {
      _repo = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
    }

    [HttpGet()]
    public IActionResult GetAuthors()
    {
      var authorsFromRepo = _repo.GetAuthors();

      return Ok(authorsFromRepo);
    }

    [HttpGet("{authorId}")]
    public IActionResult GetAuthor(Guid authorId)
    {
      var authorFromRepo = _repo.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return Ok(authorFromRepo);
    }
  }
}