using Microsoft.AspNetCore.Mvc;
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
        [Route("GetFineAsync")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _fineService.GetFineAsync());
        }

        [HttpGet]
        [Route("GetFineBorrowerByIdAsync/{borrowerId}")]
        public async Task<ActionResult> GetFineBorrowerByIdAsync(Guid borrowerId)
        {
            return Ok(await _fineService.GetFineBorrowerByIdAsync(borrowerId));
        }

    }
}
