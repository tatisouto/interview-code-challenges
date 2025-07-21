using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;
using OneBeyondTest.Mock;


namespace OneBeyondTest.Controllers
{
    public class CatalogueControllerTest
    {
        private readonly CatalogueController _controller;
        private readonly Mock<ICatalogueService> _catalogueServiceMock;
        private readonly Mock<ILogger<CatalogueController>> _logger;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BookStockMock _bookStockMock;

        public CatalogueControllerTest()
        {
            _logger = new Mock<ILogger<CatalogueController>>();
            _catalogueServiceMock = new Mock<ICatalogueService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CatalogueController(_logger.Object, _catalogueServiceMock.Object, _mapperMock.Object);
            _bookStockMock = new BookStockMock();
        }

        [Fact]
        public async Task GetCatalogueAsync_ShouldReturnListOfCatalogue()
        {
            var mockCatalogue = _bookStockMock.GetListBookStock();

            var mockCatalogueDto = _bookStockMock.GetListBookStockDto();

            _catalogueServiceMock.Setup(service => service.GetCatalogueAsync()).ReturnsAsync(mockCatalogue);

            MapperMockSetupIEnumerable(mockCatalogue, mockCatalogueDto);

            var result = await _controller.GetAsync();

            _catalogueServiceMock.Verify(service => service.GetCatalogueAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<BookStockDto>>(mockCatalogue), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<BookStockDto>>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.NotNull(returnedValue);
            Assert.Equal(mockCatalogueDto, returnedValue);
            Assert.Equal(mockCatalogueDto.Count(), returnedValue.Count());
        }

        [Fact]
        public async Task GetCatalogueOnLoan_ShouldReturnListOfOnLoan()
        {
            var mockCatalogue = _bookStockMock.GetListBookStock();

            var mockCatalogueDto = _bookStockMock.GetListBookStockDto();

            _catalogueServiceMock.Setup(service => service.GetCatalogueOnLoanAsync()).ReturnsAsync(mockCatalogue);

            MapperMockSetupIEnumerable(mockCatalogue, mockCatalogueDto);

            // Act
            var result = await _controller.GetCatalogueOnLoanAsync();

            _catalogueServiceMock.Verify(service => service.GetCatalogueOnLoanAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<BookStockDto>>(mockCatalogue), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<BookStockDto>>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(mockCatalogueDto, returnedValue);
            Assert.Equal(mockCatalogueDto.Count(), returnedValue.Count());

        }

        private void MapperMockSetupIEnumerable(IEnumerable<BookStock> mockCatalogue, List<BookStockDto> mockCatalogueDto)
        {
            _mapperMock
              .Setup(mapper => mapper.Map<IEnumerable<BookStockDto>>(mockCatalogue))
              .Returns(mockCatalogueDto);
        }

        [Fact]
        public async Task CloseCatalogueLoan_ReturnOk_WhenUpdateSucceeds()
        {
            var mockCatalogueDto = _bookStockMock.GetBookStockDto();
            var mockCatalogue = _bookStockMock.GetBookStock();
            var returnedDate = DateTime.Now;

            _catalogueServiceMock.Setup(service => service.CloseCatalogueLoanAsync(mockCatalogueDto.Id, returnedDate)).ReturnsAsync(mockCatalogue);
            MapperMockSetup(mockCatalogueDto, mockCatalogue);

            var result = await _controller.CloseCatalogueLoanAsync(mockCatalogueDto.Id, returnedDate);

            _catalogueServiceMock.Verify(service => service.CloseCatalogueLoanAsync(mockCatalogueDto.Id, returnedDate), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<BookStockDto>(mockCatalogue), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<BookStockDto>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(mockCatalogueDto, returnedValue);
            Assert.Equal(mockCatalogueDto.Id, returnedValue.Id);

        }

        private void MapperMockSetup(BookStockDto mockCatalogueDto, BookStock mockCatalogue)
        {
            _mapperMock
                        .Setup(mapper => mapper.Map<BookStockDto>(mockCatalogue))
                        .Returns(mockCatalogueDto);
        }

    }
}
