using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _reservationService;

        public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("GetReservationsAsync")]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _reservationService.GetReservationsAsync());
        }

        [HttpPost]
        [Route("ReserveBookAsync")]
        public async Task<ActionResult> PostAsync(string bookName, string email)
        {
            if (string.IsNullOrWhiteSpace(bookName) || string.IsNullOrWhiteSpace(email))
                return BadRequest("Invalid input parameters.");

            return Ok(await _reservationService.ReserveBookAsync(bookName, email));
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
            return Ok(await _reservationService.GetReserveAvailableAsync(bookId, borrowerId));
        }

    }
}
