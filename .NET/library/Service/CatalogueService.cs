using OneBeyondApi.DataAccess.Repository.Interface;
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
        public async Task<BookStock> CloseCatalogueLoanAsync(Guid id, DateTime returnedDate)
        {
            var catalogue = await _catalogueRepository.GetCatalogueByIdAsync(id);

            if (catalogue == null)
                throw new ArgumentException("Catalogue null or empty.", nameof(id));

            if (returnedDate > catalogue.LoanEndDate)
            {
                if (catalogue.OnLoanTo == null)
                    throw new ArgumentException("Borrower information is missing for this loan.", nameof(id));

                AddFine(returnedDate, catalogue.LoanEndDate.Value.Date, catalogue.OnLoanTo);
            }

            return await _catalogueRepository.CloseCatalogueLoanAsync(id);


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

            _fineRepository.AddFineAsync(fine);

        }


        public async Task<IEnumerable<BookStock>> GetCatalogueAsync()
        {
            return await _catalogueRepository.GetCatalogueAsync();
        }

        /// <summary>
        /// Retrieves the catalogue entry for a specific book based on its unique identifier.
        /// </summary>       
        public async Task<BookStock?> GetCatalogueByIdAsync(Guid id)
        {
            return await _catalogueRepository.GetCatalogueByIdAsync(id);
        }

        /// <summary>
        /// Retrieves a list of books currently on loan.
        /// </summary>        
        public async Task<IEnumerable<BookStock>> GetCatalogueOnLoanAsync()
        {
            return await _catalogueRepository.GetCatalogueOnLoanAsync();
        }

        public async Task<IEnumerable<BookStock>> SearchCatalogueAsync(CatalogueSearch search)
        {
            return await _catalogueRepository.SearchCatalogueAsync(search);
        }
    }
}
