using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
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
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
    {
      var authorsFromRepo = _repo.GetAuthors();
      var authors = new List<AuthorDto>();

      foreach (var author in authorsFromRepo)
      {
        authors.Add(new AuthorDto()
        {
          Id = author.Id,
          Name = $"{author.FirstName} {author.LastName}",
          MainCategory = author.MainCategory,
          Age = author.DateOfBirth.GetCurrentAge()
        });
      }

      return Ok(authors);
    }

    [HttpGet("{authorId}")]
    public ActionResult<AuthorDto> GetAuthor(Guid authorId)
    {
      var authorFromRepo = _repo.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      var author = new AuthorDto()
      {
        Id = authorFromRepo.Id,
        Name = $"{authorFromRepo.FirstName} {authorFromRepo.LastName}",
        MainCategory = authorFromRepo.MainCategory,
        Age = authorFromRepo.DateOfBirth.GetCurrentAge()
      };

      return Ok(author);
    }
  }
}