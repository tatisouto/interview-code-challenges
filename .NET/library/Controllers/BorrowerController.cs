using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerService _borrowerService;
        private readonly IMapper _mapper;

        public BorrowerController(ILogger<BorrowerController> logger, IBorrowerService borrowerService, IMapper mapper)
        {
            _logger = logger;
            _borrowerService = borrowerService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetBorrowersAsync")]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _borrowerService.GetBorrowersAsync();

            var resultsDto = _mapper.Map<IEnumerable<BorrowerDto>>(result);

            return Ok(resultsDto);
        }

        [HttpPost]
        [Route("AddBorrowerAsync")]
        public async Task<ActionResult> PostAsync([FromBody] BorrowerDto requestBorrower)
        {
            var request = _mapper.Map<Borrower>(requestBorrower);

            var result = await _borrowerService.AddBorrowerAsync(request);

            return Ok(result);
        }
    }
}