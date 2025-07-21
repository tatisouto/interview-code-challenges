using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;
using OneBeyondTest.Mock;
using System.Threading.Tasks;

namespace OneBeyondTest.Controllers
{
    public class CatalogueControllerTest
    {
        private readonly CatalogueController _controller;
        private readonly Mock<ICatalogueService> _catalogueService;
        private readonly Mock<ILogger<CatalogueController>> _logger;
        private readonly BookStockMock _bookStockMock;

        public CatalogueControllerTest()
        {
            _logger = new Mock<ILogger<CatalogueController>>();
            _catalogueService = new Mock<ICatalogueService>();
            _controller = new CatalogueController(_logger.Object, _catalogueService.Object);
            _bookStockMock = new BookStockMock();
        }

        [Fact]
        public async Task GetCatalogue_ShouldReturnListOfCatalogue()
        {
            var mockCatalogue = _bookStockMock.getListBookStock();

            _catalogueService.Setup(service => service.GetCatalogueAsync()).ReturnsAsync(mockCatalogue);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<BookStock>>(okResult.Value);
            Assert.Equal(mockCatalogue, returnedValue);
        }

        [Fact]
        public async Task GetCatalogueOnLoan_ShouldReturnListOfOnLoan()
        {
            var mockCatalogue = _bookStockMock.getListBookStock();

            _catalogueService.Setup(service => service.GetCatalogueOnLoanAsync()).ReturnsAsync(mockCatalogue);

            // Act
            var result = await _controller.GetCatalogueOnLoanAsync();

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<BookStock>>(okResult.Value);
            Assert.Equal(mockCatalogue, returnedValue);
        }

        [Fact]
        public async Task CloseCatalogueLoan_ReturnOk_WhenUpdateSucceeds()
        {
            var mockCatalogue = _bookStockMock.GetBookStockDto();

            var returnedDate = DateTime.Now;
            var id = Guid.NewGuid();

            _catalogueService.Setup(service => service.CloseCatalogueLoanAsync(id, returnedDate)).ReturnsAsync(mockCatalogue);

            var result = await _controller.CloseCatalogueLoanAsync(id, returnedDate);

            Assert.NotNull(result);

        }


    }
}
