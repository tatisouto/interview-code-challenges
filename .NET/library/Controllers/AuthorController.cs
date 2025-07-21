using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet]
        [Route("GetAuthorsAsync")]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _authorService.GetAuthorsAsync());
        }

        [HttpPost]
        [Route("AddAuthorAsync")]
        public async Task<ActionResult> PostAsync(Author author)
        {
            return Ok(await _authorService.AddAuthorAsync(author));
        }
    }
}