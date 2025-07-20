using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Service
{
    public class CatalogueService : ICatalogueService
    {
        private readonly ICatalogueRepository _catalogueRepository;
        private readonly IFineRepository _fineRepository;
        private const decimal FINE = 2.50m;

        public CatalogueService(ICatalogueRepository catalogueRepository, IFineRepository fineRepository)
        {
            _catalogueRepository = catalogueRepository;
            _fineRepository = fineRepository;
        }

        /// <summary>
        /// Updates the loan end date for a specific catalogue item and applies a fine if the returned date exceeds the
        /// original loan end date.
        /// </summary>        
        public BookStock CloseCatalogueLoan(Guid id, DateTime returnedDate)
        {
            var catalogue = _catalogueRepository.GetCatalogueById(id);

            if (catalogue == null)
                throw new ArgumentException("Catalogue null or empty.", nameof(id));

            if (returnedDate > catalogue.LoanEndDate)
            {
                if (catalogue.OnLoanTo == null)
                    throw new ArgumentException("Borrower information is missing for this loan.", nameof(id));

                AddFine(returnedDate, catalogue.LoanEndDate.Value.Date, catalogue.OnLoanTo);
            }

            return _catalogueRepository.CloseCatalogueLoan(id);


        }

        /// <summary>
        /// Calculates and adds a fine for a late book return.
        /// </summary>              
        private void AddFine(DateTime returnedDate, DateTime LoanEndDate, Borrower borrewer)
        {

            int daysLate = (returnedDate.Date - LoanEndDate.Date).Days;
            decimal fineAmount = daysLate * FINE;

            var fine = new Fine
            {
                Borrower = borrewer,
                Amount = fineAmount
            };

            _fineRepository.AddFines(fine);

        }


        public List<BookStock> GetCatalogue()
        {
            return _catalogueRepository.GetCatalogue();
        }

        /// <summary>
        /// Retrieves the catalogue entry for a specific book based on its unique identifier.
        /// </summary>       
        public BookStock? GetCatalogueById(Guid id)
        {
            return _catalogueRepository.GetCatalogueById(id);
        }

        /// <summary>
        /// Retrieves a list of books currently on loan.
        /// </summary>        
        public List<BookStock> GetCatalogueOnLoan()
        {
            return _catalogueRepository.GetCatalogueOnLoan();
        }

        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            return _catalogueRepository.SearchCatalogue(search);
        }
    }
}
