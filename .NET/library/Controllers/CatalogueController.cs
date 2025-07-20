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
        [Route("GetCatalogue")]
        public IList<BookStock> Get()
        {
            return _catalogueService.GetCatalogue();
        }

        [HttpPost]
        [Route("SearchCatalogue")]
        public IList<BookStock> Post(CatalogueSearch search)
        {
            return _catalogueService.SearchCatalogue(search);
        }

        /// <summary>
        /// Get details of all borrowers with active loans and the titles of books they have on loan.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCatalogueOnLoan")]
        public ActionResult<IList<BookStock>> GetCatalogueOnLoan()
        {
            try
            {
                return Ok(_catalogueService.GetCatalogueOnLoan());
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error retrieving catalogue loans.");
                return StatusCode(500, "An error occurrer");
            }


        }

        /// <summary>
        /// on loan to be returned
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnedDate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("CloseCatalogueLoan")]
        public ActionResult<BookStock> CloseCatalogueLoan(Guid id, DateTime returnedDate)
        {
            try
            {
                if (id == Guid.Empty || returnedDate == default)
                {
                    return BadRequest("Invalid input parameters.");
                }

                return _catalogueService.CloseCatalogueLoan(id, returnedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing catalogue loan.");
                return StatusCode(500, "An error occurrer");
            }

        }
    }
}