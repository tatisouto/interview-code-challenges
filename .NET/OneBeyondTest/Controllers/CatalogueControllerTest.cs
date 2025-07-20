using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondTest.Controllers
{
    public class CatalogueControllerTest
    {
        private readonly CatalogueController _controller;
        private readonly Mock<ICatalogueService> _catalogueService;
        private readonly Mock<ILogger<CatalogueController>> _logger;


        public CatalogueControllerTest()
        {
            _logger = new Mock<ILogger<CatalogueController>>();
            _catalogueService = new Mock<ICatalogueService>();
            _controller = new CatalogueController(_logger.Object, _catalogueService.Object);
        }

        [Fact]
        public void GetCatalogue_ShouldReturnListOfCatalogue()
        {
            var mockCatalogue = new List<BookStock>
            {

            };

            _catalogueService.Setup(service => service.GetCatalogue()).Returns(mockCatalogue);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IList<BookStock>>>(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var reservations = Assert.IsType<List<BookStock>>(actionResult.Value);
            Assert.Equal(mockCatalogue.Count, reservations.Count);
        }

        [Fact]
        public void GetCatalogueOnLoan_ShouldReturnListOfOnLoan()
        {
            var mockCatalogue = new List<BookStock>
            {

            };

            _catalogueService.Setup(service => service.GetCatalogueOnLoan()).Returns(mockCatalogue);

            // Act
            var result = _controller.GetCatalogueOnLoan();

            Assert.NotNull(result);
            Assert.IsType<ActionResult<IList<BookStock>>>(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var reservations = Assert.IsType<List<BookStock>>(actionResult.Value);
            Assert.Equal(mockCatalogue.Count, reservations.Count);
        }

        [Fact]
        public void CloseCatalogueLoan_ReturnOk_WhenUpdateSucceeds()
        {
            var mockCatalogue = new BookStock { };

            var returnedDate = DateTime.Now;
            var id = Guid.NewGuid();

            _catalogueService.Setup(service => service.CloseCatalogueLoan(id, returnedDate)).Returns(mockCatalogue);

            var result = _controller.CloseCatalogueLoan(id, returnedDate);

            Assert.NotNull(result);
            Assert.IsType<ActionResult<BookStock>>(result);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(actionResult.Value, id);
            Assert.Equal(actionResult.StatusCode, 200);
        }


    }
}
