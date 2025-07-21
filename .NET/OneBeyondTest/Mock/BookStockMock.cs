
using AutoFixture;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;

namespace OneBeyondTest.Mock
{
    public class BookStockMock
    {
        private readonly Fixture _fixture;
        public BookStockMock()
        {
            _fixture = new Fixture();

        }
        public BookStockDto GetBookStockDto()
        {
            return _fixture.Build<BookStockDto>().Create();
        }

        public BookStock GetBookStock()
        {
            return _fixture.Build<BookStock>().Create();
        }

        public List<BookStockDto> GetListBookStockDto()
        {
            return _fixture.Build<BookStockDto>().CreateMany(3).ToList();
        }

        public List<BookStock> GetListBookStock()
        {
            return _fixture.Build<BookStock>().CreateMany(3).ToList();
        }
    }
}
