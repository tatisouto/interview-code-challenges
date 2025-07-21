using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;
using System.Threading.Tasks;

namespace OneBeyondApi.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Guid> AddBookAsync(Book book)
        {
            return await _bookRepository.AddBookAsync(book);
        }
        public async Task<Book?> GetBookByNameAsync(string bookName)
        {
            return await _bookRepository.GetBookByNameAsync(bookName);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetBooksAsync();
        }
    }
}

