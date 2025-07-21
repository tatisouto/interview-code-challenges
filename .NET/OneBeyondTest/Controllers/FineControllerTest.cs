using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OneBeyondApi.Controllers;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;
using OneBeyondTest.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondTest.Controllers
{
    public class FineControllerTest
    {
        private readonly FineController _controller;
        private readonly Mock<IFineService> _fineServiceMock;
        private readonly Mock<ILogger<FineController>> _logger;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FineMock _fineMock;

        public FineControllerTest()
        {
            _logger = new Mock<ILogger<FineController>>();
            _fineServiceMock = new Mock<IFineService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new FineController(_logger.Object, _fineServiceMock.Object, _mapperMock.Object);
            _fineMock = new FineMock();
        }


        [Fact]
        public async Task GetAsync_ShouldReturnListOfFine()
        {
            var mockFine = _fineMock.GetListFine();

            var mockFineDto = _fineMock.GetListFineDto();

            _fineServiceMock.Setup(service => service.GetFineAsync()).ReturnsAsync(mockFine);

            MapperMockSetupIEnumerable(mockFine, mockFineDto);

            var result = await _controller.Get();

            _fineServiceMock.Verify(service => service.GetFineAsync(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<FineDto>>(mockFine), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<List<FineDto>>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(mockFineDto.First().BorrowerName, returnedValue.First().BorrowerName);

        }

        [Fact]
        public async Task GetFineBorrowerByIdAsync_ShouldReturn()
        {
            var mockFine = _fineMock.GetFine();

            var mockFineDto = _fineMock.GetFineDto();

            _fineServiceMock.Setup(service => service.GetFineBorrowerByIdAsync(mockFineDto.Id)).ReturnsAsync(mockFine);

            MapperMockSetup(mockFineDto, mockFine);

            var result = await _controller.GetFineBorrowerByIdAsync(mockFineDto.Id);

            _fineServiceMock.Verify(service => service.GetFineBorrowerByIdAsync(mockFineDto.Id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<FineDto>(mockFine), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedValue = Assert.IsType<FineDto>(okResult.Value);

            Assert.NotNull(returnedValue);
            Assert.Equal(mockFineDto.BorrowerName, returnedValue.BorrowerName);
        }

        private void MapperMockSetupIEnumerable(IEnumerable<Fine> mock, List<FineDto> mockDto)
        {
            _mapperMock
              .Setup(mapper => mapper.Map<IEnumerable<FineDto>>(mock))
              .Returns(mockDto);
        }


        private void MapperMockSetup(FineDto mockDto, Fine mock)
        {
            _mapperMock
                        .Setup(mapper => mapper.Map<FineDto>(mock))
                        .Returns(mockDto);
        }

    }
}
