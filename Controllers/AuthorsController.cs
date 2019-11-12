using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
  [ApiController]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _repo;

    public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
    {
      _repo = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
    }

    public IActionResult GetAuthors()
    {
      var authorsFromRepo = _repo.GetAuthors();

      return new JsonResult(authorsFromRepo);
    }
  }
}