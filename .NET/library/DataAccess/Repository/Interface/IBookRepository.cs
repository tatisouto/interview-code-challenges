using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Guid> AddBookAsync(Book book);

        Task<Book?> GetBookByNameAsync(string bookName);
    }
}
