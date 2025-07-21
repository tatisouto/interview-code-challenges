using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;
using OneBeyondTest.Mock;

namespace OneBeyondTest.Controllers
{
    public class ReservationControllerTest
    {
        private readonly ReservationController _controller;
        private readonly Mock<IReservationService> _reservationServiceMock;
        private readonly Mock<ILogger<ReservationController>> _logger;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReserveMock _reserveBookMock;

        public ReservationControllerTest()
        {
            _logger = new Mock<ILogger<ReservationController>>();
            _reservationServiceMock = new Mock<IReservationService>();
            _mapperMock = new Mock<IMapper>();

            _controller = new ReservationController(_logger.Object, _reservationServiceMock.Object, _mapperMock.Object);
            _reserveBookMock = new ReserveMock();
        }


        [Fact]
        public async Task GetAsync_ShouldReturnOkWithMappedReservationDtoList()
        {
            // Arrange
            var reservations = _reserveBookMock.GetListReservation();

            var reservationsDto = _reserveBookMock.GetListReservationDto();

            _reservationServiceMock
                .Setup(service => service.GetReservationsAsync())
                .ReturnsAsync(reservations);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<ReservationDto>>(reservations))
                .Returns(reservationsDto);

            // Act
            var result = await _controller.GetAsync();

            _reservationServiceMock.Verify(service => service.GetReservationsAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ReservationDto>>(reservations), Times.Once);

            Assert.NotNull(result);






        }

    }
}
