using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;
using OneBeyondApi.Service.Interface;
using System.Collections.Generic;
using System.Net;

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
        public List<Reservation> GetReservations()
        {
            return _reservationRepository.GetReservations().ToList();
        }

        /// <summary>
        /// Reserves a book for a borrower, ensuring the book and borrower exist and are valid.
        /// </summary>      
        public Guid ReserveBook(string bookName, string borrowerEmailAddress)
        {
            if (string.IsNullOrWhiteSpace(bookName))
                throw new ArgumentException("Book name cannot be null or empty.", nameof(bookName));

            if (string.IsNullOrWhiteSpace(borrowerEmailAddress))
                throw new ArgumentException("Borrower email address cannot be null or empty.", nameof(borrowerEmailAddress));

            var book = _bookRepository.GetBookByName(bookName)
                ?? throw new InvalidOperationException($"Book '{bookName}' not found.");

            var borrower = _borrowerRepository.GetBorrowerByEmailAddress(borrowerEmailAddress)
                ?? throw new InvalidOperationException($"Borrower with email '{borrowerEmailAddress}' not found.");

            var getReserve = _reservationRepository.GetReservationsByBookId(book.Id);
            var existingReservation = getReserve.Any(x => x.Borrower.Id == borrower.Id);

            if (existingReservation)
                throw new InvalidOperationException($"Existing Reservation '{borrowerEmailAddress}' for '{bookName}'");

            var reservation = new Reservation
            {
                Book = book,
                Borrower = borrower,
                IsActive = true,
                ReservationDate = DateTime.UtcNow.Date,
            };

            return _reservationRepository.AddReservation(reservation);
        }

        public ReserveAvailableDto GetReserveAvailable(Guid bookId, Guid borrowerId)
        {
            var catalogue = _catalogueRepository.GetCatalogueByBookId(bookId);

            if (catalogue == null)
                throw new InvalidOperationException($"Reserve '{bookId}' not found.");


            var reservations = _reservationRepository.GetReservationsByBookId(bookId);

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

                DateTime estimatedDate = (catalogue.LoanEndDate?.Date ?? DateTime.UtcNow)
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


