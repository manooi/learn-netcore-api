using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        [HttpHead()]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorResourceParameters authorResourceParameters)
        //public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery(Name = "mainCategory")] string mainCategory, string searchQuery)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        //[HttpPost()]
        //public ActionResult<IEnumerable<AuthorDto>> GetAuthorsPost(AuthorResourceParameters authorResourceParameters)
        ////public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery(Name = "mainCategory")] string mainCategory, string searchQuery)
        //{
        //  var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorResourceParameters);
        //  return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        //}

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthor(Guid authorId)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorsFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(authorsFromRepo));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            // API Controller already handle this!!
            //if (author == null)
            //{
            //  return BadRequest();
            //}

            // Use reflection for handle {} input

            // Map AuthorForCreationDto with Author Entity
            var authorEntity = _mapper.Map<Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();

            // Map authorEntity (have id field now) backto the AuthorDto
            // to return AuthorDto
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { authorId = authorToReturn.Id }, authorToReturn);
        }

        [HttpOptions]
        public async Task<IActionResult> GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            //for testing purpose
            var bytes = Encoding.UTF8.GetBytes("Hello World");
            await Response.Body.WriteAsync(bytes);
            //return Ok();
            return new EmptyResult();
        }

        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {

            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteAuthor(authorFromRepo); // CascadeOnDelete is on by default!!
            _courseLibraryRepository.Save();
            return NoContent();


        }


    }
}