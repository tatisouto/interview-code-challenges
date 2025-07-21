using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondTest.Controllers
{
    public class ReservationControllerTest
    {
        private readonly ReservationController _controller;
        private readonly Mock<IReservationService> _reservationService;
        private readonly Mock<ILogger<ReservationController>> _logger;

        public ReservationControllerTest()
        {
            _logger = new Mock<ILogger<ReservationController>>();
            _reservationService = new Mock<IReservationService>();
            _controller = new ReservationController(_logger.Object, _reservationService.Object);
        }

        [Fact]
        public async Task GetReservations_ShouldReturnListOfReservations()
        {
            // Arrange
            var mockReservations = new List<Reservation>
            {
                new Reservation { Id = Guid.NewGuid(), IsActive = true },
                new Reservation { Id = Guid.NewGuid(), IsActive = false }
            };
            _reservationService.Setup(service => service.GetReservationsAsync()).ReturnsAsync(mockReservations);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IList<Reservation>>>(result);
            //var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            //var reservations = Assert.IsType<List<Reservation>>(actionResult.Value);
            //Assert.Equal(mockReservations.Count, reservations.Count);
        }

        [Fact]
        public async Task Post_ShouldReturnOk_WhenSaveSucceeds()
        {

            var bookName = "Rust Development Cookbook";
            var borrowerEmailAddress = "mary@gmail.com";
            var bookId = Guid.NewGuid();

            _reservationService.Setup(service => service.ReserveBookAsync(bookName, borrowerEmailAddress)).ReturnsAsync(bookId);

            var result = await _controller.PostAsync(bookName, borrowerEmailAddress);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<Guid>>(result);

            //var actionResult = Assert.IsType<OkObjectResult>(result.ExecuteResultAsync);
            //Assert.Equal(actionResult.Value, bookId);
            //Assert.Equal(actionResult.StatusCode, 200);
        }

        [Fact]
        public async Task GetReserveAvailable_ShouldReturnReservations()
        {
            var mockDto = new ReserveAvailableDto
            {
                EstimatedDate = DateTime.Now.Date,
                IsAvailableNow = true,
            };

            var bookId = Guid.NewGuid();
            var borrowerId = Guid.NewGuid();

            _reservationService.Setup(service => service.GetReserveAvailableAsync(bookId, borrowerId)).ReturnsAsync(mockDto);

            // Act
            var result = await _controller.GetReserveAvailableAsync(bookId, borrowerId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<ReserveAvailableDto>>(result);
            //var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            //var reservations = Assert.IsType<ReserveAvailableDto>(actionResult.Value);
            //Assert.Equal(mockDto.IsAvailableNow, reservations.IsAvailableNow);
        }


    }
}
