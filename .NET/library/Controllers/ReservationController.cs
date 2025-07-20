using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service;
using OneBeyondApi.Service.Interface;
using System.Xml.Linq;

namespace OneBeyondApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<CatalogueController> _logger;
        private readonly IReservationService _reservationService;

        public ReservationController(ILogger<CatalogueController> logger, IReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("GetReservations")]
        public ActionResult<IList<Reservation>> Get()
        {
            try
            {
                return Ok(_reservationService.GetReservations());
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error while reserving book.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reservations.");
                return StatusCode(500, "An error occurrer");
            }

        }


        [HttpPost]
        [Route("ReserveBook")]
        public ActionResult<Guid> Post(string bookName, string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bookName) || string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Invalid input parameters.");
                }

                return Ok(_reservationService.ReserveBook(bookName, email));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error while reserving book.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reserve book.");
                return StatusCode(500, "An error occurrer");
            }


        }

        /// <summary>
        ///  Get Reserve Available
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="borrowerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReserveAvailable/{bookId}/{borrowerId}")]
        public ActionResult<ReserveAvailableDto> GetReserveAvailable(Guid bookId, Guid borrowerId)
        {
            try
            {
                if (bookId == Guid.Empty || borrowerId == Guid.Empty)
                    return BadRequest("Invalid input parameters.");

                return Ok(_reservationService.GetReserveAvailable(bookId, borrowerId));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error while reserving book.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrive reserve.");
                return StatusCode(500, "An error occurrer");
            }


        }

    }
}
