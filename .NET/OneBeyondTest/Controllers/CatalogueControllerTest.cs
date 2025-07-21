using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Service.Interface;
using OneBeyondTest.Mock;


namespace OneBeyondTest.Controllers
{
    public class CatalogueControllerTest
    {
        private readonly CatalogueController _controller;
        private readonly Mock<ICatalogueService> _catalogueService;
        private readonly Mock<ILogger<CatalogueController>> _logger;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BookStockMock _bookStockMock;

        public CatalogueControllerTest()
        {
            _logger = new Mock<ILogger<CatalogueController>>();
            _catalogueService = new Mock<ICatalogueService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CatalogueController(_logger.Object, _catalogueService.Object, _mapperMock.Object);
            _bookStockMock = new BookStockMock();
        }

      


    }
}
