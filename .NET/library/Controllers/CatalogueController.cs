using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : ControllerBase
    {
        private readonly ILogger<CatalogueController> _logger;
        private readonly ICatalogueService _catalogueService;
        private readonly IMapper _mapper;

        public CatalogueController(ILogger<CatalogueController> logger, ICatalogueService catalogueService, IMapper mapper)
        {
            _logger = logger;
            _catalogueService = catalogueService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCatalogueAsync")]
        public async Task<ActionResult> GetAsync()
        {
            var results = await _catalogueService.GetCatalogueAsync();

            var resultsDto = _mapper.Map<IEnumerable<BookStockDto>>(results);

            return Ok(resultsDto);
        }

        [HttpPost]
        [Route("SearchCatalogueAsync")]
        public async Task<ActionResult> PostAsync([FromBody] CatalogueSearchRequestDto requestSearch)
        {
            var request = _mapper.Map<CatalogueSearch>(requestSearch);

            var results = await _catalogueService.SearchCatalogueAsync(request);

            var resultsDto = _mapper.Map<IEnumerable<CatalogueSearchRequestDto>>(results);

            return Ok(resultsDto);


        }

        /// <summary>
        /// Get details of all borrowers with active loans and the titles of books they have on loan.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCatalogueOnLoanAsync")]
        public async Task<ActionResult> GetCatalogueOnLoanAsync()
        {
            var results = await _catalogueService.GetCatalogueOnLoanAsync();

            var resultsDto = _mapper.Map<IEnumerable<BookStockDto>>(results);

            return Ok(resultsDto);
        }

        /// <summary>
        /// on loan to be returned
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnedDate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("CloseCatalogueLoanAsync/{id}/{returnedDate}")]
        public async Task<ActionResult> CloseCatalogueLoanAsync(Guid id, DateTime returnedDate)
        {
            if (id == Guid.Empty || returnedDate == default)
                return BadRequest("Invalid input parameters.");

            var results = await _catalogueService.CloseCatalogueLoanAsync(id, returnedDate);

            var resultsDto = _mapper.Map<BookStockDto>(results);

            return Ok(resultsDto);
        }
    }
}