using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService, IMapper mapper)
        {
            _logger = logger;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetReservationsAsync")]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _reservationService.GetReservationsAsync();

            var reservationDto = _mapper.Map<IEnumerable<ReservationDto>>(result);

            return Ok(reservationDto);
        }

        [HttpPost]
        [Route("ReserveBookAsync")]
        public async Task<ActionResult> PostAsync([FromBody] ReserveBookRequestDto reserveBookDto)
        {
            if (string.IsNullOrWhiteSpace(reserveBookDto.BookName) || string.IsNullOrWhiteSpace(reserveBookDto.Email))
                return BadRequest("Invalid input parameters.");

            return Ok(await _reservationService.ReserveBookAsync(reserveBookDto.BookName, reserveBookDto.Email));
        }

        /// <summary>
        ///  Get Reserve Available
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="borrowerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReserveAvailableAsync/{bookId}/{borrowerId}")]
        public async Task<ActionResult> GetReserveAvailableAsync(Guid bookId, Guid borrowerId)
        {
            var result = await _reservationService.GetReserveAvailableAsync(bookId, borrowerId);

            var reserveAvailableDto = _mapper.Map<ReserveAvailableDto>(result);

            return Ok(reserveAvailableDto);
        }

    }
}
