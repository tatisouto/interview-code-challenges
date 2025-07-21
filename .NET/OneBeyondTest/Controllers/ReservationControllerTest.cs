using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service;
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
            var mockReservations = _reserveBookMock.GetListReservation();

            var mockReservationsDto = _reserveBookMock.GetListReservationDto();

            _reservationServiceMock
                .Setup(service => service.GetReservationsAsync())
                .ReturnsAsync(mockReservations);

            _mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<ReservationDto>>(mockReservations))
                .Returns(mockReservationsDto);

            // Act
            var result = await _controller.GetAsync();

            _reservationServiceMock.Verify(service => service.GetReservationsAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ReservationDto>>(mockReservations), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<ReservationDto>>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.NotNull(returnedValue);
            Assert.Equal(mockReservationsDto, returnedValue);
            Assert.Equal(mockReservationsDto.Count(), returnedValue.Count());
        }

        [Fact]
        public async Task GetReserveAvailableAsync_ShouldReturnReservations()
        {
            var mockReserveAvailable = _reserveBookMock.GetReserveAvailable();

            var mockReserveAvailableDto = _reserveBookMock.GetReserveAvailableDto();

            var bookId = Guid.NewGuid();
            var borrowerId = Guid.NewGuid();

            _reservationServiceMock
               .Setup(service => service.GetReserveAvailableAsync(bookId, borrowerId))
               .ReturnsAsync(mockReserveAvailable);

            _mapperMock
               .Setup(mapper => mapper.Map<ReserveAvailableDto>(mockReserveAvailable))
               .Returns(mockReserveAvailableDto);

            var result = await _controller.GetReserveAvailableAsync(bookId, borrowerId);

            _reservationServiceMock.Verify(service => service.GetReserveAvailableAsync(bookId, borrowerId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ReserveAvailableDto>(mockReserveAvailable), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<ReserveAvailableDto>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(mockReserveAvailableDto, returnedValue);
            Assert.Equal(mockReserveAvailableDto.IsAvailableNow, returnedValue.IsAvailableNow);

        }

        [Fact]
        public async Task PostReserveBookAsync_ShouldReturnOk_WhenSaveSucceeds()
        {
            var bookId = Guid.NewGuid();

            var mockRequestDto = _reserveBookMock.GetReserveBookRequestDto();

            _reservationServiceMock.Setup(service => service.ReserveBookAsync(mockRequestDto.BookName, mockRequestDto.Email))
                                   .ReturnsAsync(bookId);

            var result = await _controller.PostAsync(mockRequestDto);

            _reservationServiceMock.Verify(service => service.ReserveBookAsync(mockRequestDto.BookName, mockRequestDto.Email), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<Guid>(okResult.Value);

            Assert.NotNull(result);
            Assert.Equal(bookId, returnedValue);

        }

    }
}
