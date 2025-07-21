using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAuthorsAsync")]
        public async Task<ActionResult> GetAsync()
        {

            var result = await _authorService.GetAuthorsAsync();

            var resultsDto = _mapper.Map<IEnumerable<AuthorDto>>(result);

            return Ok(resultsDto);

        }

        [HttpPost]
        [Route("AddAuthorAsync")]
        public async Task<ActionResult> PostAsync([FromBody] AuthorDto requestAuthor)
        {
            var request = _mapper.Map<Author>(requestAuthor);

            var result = await _authorService.AddAuthorAsync(request);

            return Ok(result);
        }
    }
}