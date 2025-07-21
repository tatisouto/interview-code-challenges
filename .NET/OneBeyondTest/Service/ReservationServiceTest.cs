using Moq;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;
using OneBeyondApi.Service;
using OneBeyondTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondTest.Service
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IBorrowerRepository> _borrowerRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<ICatalogueRepository> _catalogueRepositoryMock;
        private readonly ReservationService _reservationServiceMock;
        private readonly ReserveMock _reserveBookMock;
        

        public ReservationServiceTest()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _borrowerRepositoryMock = new Mock<IBorrowerRepository>();
            _catalogueRepositoryMock = new Mock<ICatalogueRepository>();
            _reserveBookMock = new ReserveMock();

            _reservationServiceMock = new ReservationService(_reservationRepositoryMock.Object, _borrowerRepositoryMock.Object, _bookRepositoryMock.Object, _catalogueRepositoryMock.Object);
        }

        [Fact]
        public async Task GetReservationsAsync_ShouldReturnReservationsFromRepository()
        {
            // Arrange
            var reservations = _reserveBookMock.GetListReservation();

            _reservationRepositoryMock
                .Setup(repo => repo.GetReservationsAsync())
                .ReturnsAsync(reservations);

            // Act
            var result = await _reservationServiceMock.GetReservationsAsync();

            _reservationRepositoryMock.Verify(repo => repo.GetReservationsAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(result.Count(), reservations.Count());
        }



    }
}

