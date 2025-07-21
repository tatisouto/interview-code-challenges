using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : ControllerBase
    {
        private readonly ILogger<CatalogueController> _logger;
        private readonly ICatalogueService _catalogueService;

        public CatalogueController(ILogger<CatalogueController> logger, ICatalogueService catalogueService)
        {
            _logger = logger;
            _catalogueService = catalogueService;
        }

        [HttpGet]
        [Route("GetCatalogueAsync")]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _catalogueService.GetCatalogueAsync());
        }

        [HttpPost]
        [Route("SearchCatalogueAsync")]
        public async Task<ActionResult> PostAsync(CatalogueSearch search)
        {
            return Ok(await _catalogueService.SearchCatalogueAsync(search));
        }

        /// <summary>
        /// Get details of all borrowers with active loans and the titles of books they have on loan.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCatalogueOnLoanAsync")]
        public async Task<ActionResult> GetCatalogueOnLoanAsync()
        {
            return Ok(await _catalogueService.GetCatalogueOnLoanAsync());
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

            return Ok(await _catalogueService.CloseCatalogueLoanAsync(id, returnedDate));
        }
    }
}