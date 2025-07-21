using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet]
        [Route("GetBooksAsync")]
        public async Task<ActionResult> GetBooksAsync()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        [HttpPost]
        [Route("AddBookAsync")]
        public async Task<ActionResult> PostAsync(Book book)
        {
            return Ok(await _bookService.AddBookAsync(book));
        }
    }
}