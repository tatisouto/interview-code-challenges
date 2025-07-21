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
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetBooksAsync")]
        public async Task<ActionResult> GetBooksAsync()
        {
            var result = await _bookService.GetBooksAsync();

            var resultsDto = _mapper.Map<IEnumerable<BookDto>>(result);

            return Ok(resultsDto);
        }

        [HttpPost]
        [Route("AddBookAsync")]
        public async Task<ActionResult> PostAsync([FromBody] BookDto requestBook)
        {
            var request = _mapper.Map<Book>(requestBook);

            var result = await _bookService.AddBookAsync(request);

            return Ok(result);
        }
    }
}