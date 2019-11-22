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
      if (!_repo.AuthorExists(authorId))
      {
        return NotFound();
      }

      var authorFromRepo = _repo.GetAuthor(authorId);

      return Ok(authorFromRepo);
    }
  }
}