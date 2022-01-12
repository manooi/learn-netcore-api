using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
  [ApiController]
  //[Route("api/[controller]")]
  [Route("api/authors")]
  public class AuthorsController : ControllerBase // Controller
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;

    public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
    {
      _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    //[HttpGet("api/authors")]
    [HttpGet()]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors();
      return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
    }

    [HttpGet("{authorId}")]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthor(Guid authorId)
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthor(authorId);
      if (authorsFromRepo == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<AuthorDto>(authorsFromRepo));
    }
  }
}