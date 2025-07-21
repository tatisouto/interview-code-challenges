using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerService _borrowerService;

        public BorrowerController(ILogger<BorrowerController> logger, IBorrowerService borrowerService)
        {
            _logger = logger;
            _borrowerService = borrowerService;
        }

        [HttpGet]
        [Route("GetBorrowersAsync")]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _borrowerService.GetBorrowersAsync());
        }

        [HttpPost]
        [Route("AddBorrowerAsync")]
        public async Task<ActionResult> PostAsync(Borrower borrower)
        {
            return Ok(await _borrowerService.AddBorrowerAsync(borrower));
        }
    }
}