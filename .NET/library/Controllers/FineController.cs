using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly ILogger<FineController> _logger;
        private readonly IFineService _fineService;
        private readonly IMapper _mapper;

        public FineController(ILogger<FineController> logger, IFineService fineService, IMapper mapper)
        {
            _logger = logger;
            _fineService = fineService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetFineAsync")]
        public async Task<ActionResult> Get()
        {
            var result = await _fineService.GetFineAsync();

            var fineDto = _mapper.Map<IEnumerable<FineDto>>(result);

            return Ok(fineDto);
           
        }

        [HttpGet]
        [Route("GetFineBorrowerByIdAsync/{borrowerId}")]
        public async Task<ActionResult> GetFineBorrowerByIdAsync(Guid borrowerId)
        {
            var result = await _fineService.GetFineBorrowerByIdAsync(borrowerId);

            var fineDto = _mapper.Map<FineDto>(result);

            return Ok(fineDto);
        }

    }
}
