using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }
        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public BookStock? GetCatalogueById(Guid id)
        {
            using (var context = new LibraryContext())
            {
                return context.Catalogue
               .Include(x => x.Book)
               .ThenInclude(x => x.Author)
               .Include(x => x.OnLoanTo).FirstOrDefault(x => x.Id == id);
            }
        }


        public List<BookStock> GetCatalogueOnLoan()
        {
            using (var context = new LibraryContext())
            {
                return context.Catalogue
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.OnLoanTo)
                        .Where(onloan => onloan.OnLoanTo != null).ToList();
            }
        }

        public BookStock? GetCatalogueByBookId(Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                return context.Catalogue
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.OnLoanTo)
                        .FirstOrDefault(x=>x.Book.Id == bookId);
            }
        }


        public BookStock CloseCatalogueLoan(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var book = GetCatalogueById(id);

                if (book == null)
                    return null;

                book.OnLoanTo = null;
                book.LoanEndDate = null;

                context.Catalogue.Update(book);

                context.SaveChanges();
                return book;
            }
        }


        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author))
                    {
                        list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName))
                    {
                        list = list.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }

                return list.ToList();
            }
        }
    }
}
