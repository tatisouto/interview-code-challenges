
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

        public List<BookStockDto> getListBookStock()
        {
            return _fixture.Build<BookStockDto>().CreateMany(3).ToList();
        }
    }
}
