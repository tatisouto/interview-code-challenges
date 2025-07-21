using OneBeyondApi.Model;

namespace OneBeyondApi.Service.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Guid> AddBookAsync(Book book);
        Task<Book?> GetBookByNameAsync(string bookName);
    }
}
