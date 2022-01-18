using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthorCollectionsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;

    public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
    {
      _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("({ids})", Name = "GetAuthorCollection")]
    public IActionResult GetAuthorCollection(
    [FromRoute]
    [ModelBinder(BinderType = typeof(ArrayModelBinder))]
    IEnumerable<Guid> ids)
    // Use custom model binding for IEnumberable<Guid>
    {
      if (ids == null)
      {
        return BadRequest();
      }

      ids = ids.Where(id => (id != Guid.Empty)).ToList().AsEnumerable();

      var authorEntities = _courseLibraryRepository.GetAuthors(ids);
      if (ids.Count() != authorEntities.Count())
      {
        return NotFound();
      }

      var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
      return Ok(authorsToReturn);
    }

    [HttpPost]
    public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorCollection)
    {
      var authorEntities = _mapper.Map<IEnumerable<Author>>(authorCollection);
      foreach (var author in authorEntities)
      {
        _courseLibraryRepository.AddAuthor(author);
      }

      _courseLibraryRepository.Save();

      // We need array key to return

      var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
      var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
      return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);
    }
  }
}