using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Books
                    .ToListAsync();
            }
        }


        public async Task<Book?> GetBookByNameAsync(string bookName)
        {
            using (var context = new LibraryContext())
            {
                return await context.Books
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Name.Contains(bookName));
            }
        }

        public async Task<Guid> AddBookAsync(Book book)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(book);
                await context.SaveChangesAsync();
                return book.Id;
            }
        }
    }
}
