using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }
        public async Task<IEnumerable<BookStock>> GetCatalogueAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Catalogue
                     .Include(x => x.Book)
                     .ThenInclude(x => x.Author)
                     .Include(x => x.OnLoanTo)
                     .ToListAsync();

            }
        }

        public async Task<BookStock?> GetCatalogueByIdAsync(Guid id)
        {
            using (var context = new LibraryContext())
            {
                return await context.Catalogue
               .Include(x => x.Book)
               .ThenInclude(x => x.Author)
               .Include(x => x.OnLoanTo).FirstOrDefaultAsync(x => x.Id == id);
            }
        }


        public async Task<IEnumerable<BookStock>> GetCatalogueOnLoanAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Catalogue
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.OnLoanTo)
                        .Where(onloan => onloan.OnLoanTo != null).ToListAsync();
            }
        }

        public async Task<BookStock?> GetCatalogueByBookIdAsync(Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                return await context.Catalogue
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.OnLoanTo)
                        .FirstOrDefaultAsync(x => x.Book.Id == bookId);
            }
        }


        public async Task<BookStock> CloseCatalogueLoanAsync(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var book = await GetCatalogueByIdAsync(id);

                if (book == null)
                    return null;

                book.OnLoanTo = null;
                book.LoanEndDate = null;

                context.Catalogue.Update(book);

                await context.SaveChangesAsync();
                return book;
            }
        }


        public async Task<IEnumerable<BookStock>> SearchCatalogueAsync(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var query = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author))
                    {
                        query = query.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName))
                    {
                        query = query.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }

                return await query.ToListAsync();
            }
        }
    }
}
