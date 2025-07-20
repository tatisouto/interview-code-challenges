using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Service;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly ILogger<FineController> _logger;
        private readonly IFineService _fineService;

        public FineController(ILogger<FineController> logger, IFineService fineService)
        {
            _logger = logger;
            _fineService = fineService;
        }

        [HttpGet]
        [Route("GetFine")]
        public ActionResult<IList<Fine>> Get()
        {
            try
            {
                return Ok(_fineService.GetFine());
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error fines.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fines.");
                return StatusCode(500, "An error occurrer");

            }

        }

        [HttpGet]
        [Route("GetFineBorrowerById/{borrowerId}")]
        public ActionResult<Fine> GetFineBorrowerById(Guid borrowerId)
        {
            try
            {
                return Ok(_fineService.GetFineBorrowerById(borrowerId));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error fines.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fines.");
                return StatusCode(500, "An error occurrer");

            }

        }

    }
}
