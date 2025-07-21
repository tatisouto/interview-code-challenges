using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface ICatalogueRepository
    {
        Task<IEnumerable<BookStock>> GetCatalogueAsync();
        Task<BookStock?> GetCatalogueByIdAsync(Guid id);
        Task<IEnumerable<BookStock>> GetCatalogueOnLoanAsync();
        Task<IEnumerable<BookStock>> SearchCatalogueAsync(CatalogueSearch search);
        Task<BookStock> CloseCatalogueLoanAsync(Guid id);
        Task<BookStock?> GetCatalogueByBookIdAsync(Guid bookId);
    }
}
