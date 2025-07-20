using OneBeyondApi.Model;

namespace OneBeyondApi.Service.Interface
{
    public interface ICatalogueService
    {
        public List<BookStock> GetCatalogue();
        public BookStock? GetCatalogueById(Guid id);
        public List<BookStock> GetCatalogueOnLoan();
        public List<BookStock> SearchCatalogue(CatalogueSearch search);
        public BookStock CloseCatalogueLoan(Guid Id, DateTime returnedDate);

    }
}
