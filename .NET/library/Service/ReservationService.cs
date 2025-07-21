using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Service
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICatalogueRepository _catalogueRepository;
        private const int AVG_LOAN_DAYS = 14;

        public ReservationService(IReservationRepository reservationRepository, IBorrowerRepository borrowerRepository, IBookRepository bookRepository, ICatalogueRepository catalogueRepository)
        {
            _reservationRepository = reservationRepository;
            _borrowerRepository = borrowerRepository;
            _bookRepository = bookRepository;
            _catalogueRepository = catalogueRepository;
        }

        /// <summary>
        /// Retrieves a list of all reservations.
        /// </summary> 
        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _reservationRepository.GetReservationsAsync();
        }

        /// <summary>
        /// Reserves a book for a borrower, ensuring the book and borrower exist and are valid.
        /// </summary>      
        public async Task<Guid> ReserveBookAsync(string bookName, string borrowerEmailAddress)
        {
            if (string.IsNullOrWhiteSpace(bookName))
                throw new ArgumentException("Book name cannot be null or empty.", nameof(bookName));

            if (string.IsNullOrWhiteSpace(borrowerEmailAddress))
                throw new ArgumentException("Borrower email address cannot be null or empty.", nameof(borrowerEmailAddress));

            var book = await _bookRepository.GetBookByNameAsync(bookName)
                ?? throw new InvalidOperationException($"Book '{bookName}' not found.");

            var borrower = await _borrowerRepository.GetBorrowerByEmailAddressAsync(borrowerEmailAddress)
                ?? throw new InvalidOperationException($"Borrower with email '{borrowerEmailAddress}' not found.");

            var getReserve = await _reservationRepository.GetReservationsByBookIdAsync(book.Id);
            var existingReservation = getReserve.Any(x => x.Borrower.Id == borrower.Id);

            if (existingReservation)
                throw new InvalidOperationException($"Existing Reservation '{borrowerEmailAddress}' for '{bookName}'");

            var reservation = new Reservation
            {
                Book = book,
                Borrower = borrower,
                IsActive = true
            };

            return await _reservationRepository.AddReservationAsync(reservation);
        }

        public async Task<ReserveAvailableDto> GetReserveAvailableAsync(Guid bookId, Guid borrowerId)
        {
            var catalogue = await _catalogueRepository.GetCatalogueByBookIdAsync(bookId);

            if (catalogue == null)
                throw new InvalidOperationException($"Reserve '{bookId}' not found.");

            var reservations = await _reservationRepository.GetReservationsByBookIdAsync(bookId);

            var position = reservations
                    .OrderBy(r => r.ReservationDate)
                    .Select((r, index) => new { r.Borrower.Id, Position = index + 1 })
                    .FirstOrDefault(x => x.Id == borrowerId)?.Position ?? 0;


            if (catalogue.OnLoanTo == null && position == 0)
            {
                return new ReserveAvailableDto
                {
                    IsAvailableNow = true,
                    EstimatedDate = DateTime.Now.Date
                };
            }
            else
            {
                int peopleAhead = position - 1;

                DateTime estimatedDate = (catalogue.LoanEndDate?.Date ?? DateTime.Now.Date)
                                         .AddDays(peopleAhead * AVG_LOAN_DAYS);

                return new ReserveAvailableDto
                {
                    IsAvailableNow = false,
                    EstimatedDate = estimatedDate
                };
            }
        }
    }
}


