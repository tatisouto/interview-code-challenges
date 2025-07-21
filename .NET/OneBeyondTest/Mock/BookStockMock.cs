
using AutoFixture;
using OneBeyondApi.Model;

namespace OneBeyondTest.Mock
{
    public class BookStockMock
    {
        private readonly Fixture _fixture;
        public BookStockMock()
        {
            _fixture = new Fixture();

        }
        public BookStock GetBookStockDto()
        {
            return _fixture.Build<BookStock>().Create();
        }

        public List<BookStock> getListBookStock()
        {
            return _fixture.Build<BookStock>().CreateMany(3).ToList();
        }
    }
}
