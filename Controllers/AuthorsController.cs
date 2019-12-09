using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
                              IMapper mapper)
    {
      _repo = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet()]
    [HttpHead()] // HEAD is the same as GET but will not return a response body.
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] string mainCategory)
    {
      var authorsFromRepo = _repo.GetAuthors(mainCategory);

      return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
    }

    [HttpGet("{authorId}")]
    public ActionResult<AuthorDto> GetAuthor(Guid authorId)
    {
      var authorFromRepo = _repo.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
    }
  }
}