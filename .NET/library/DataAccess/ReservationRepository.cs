using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {

        public ReservationRepository() { }

        public Guid AddReservation(Reservation reservation)
        {
            using (var context = new LibraryContext())
            {
                context.Attach(reservation.Borrower);
                context.Attach(reservation.Book);

                context.Reservations.Add(reservation);
                context.SaveChanges();
                return reservation.Id;
            }
        }

        public List<Reservation> GetReservations()
        {
            using (var context = new LibraryContext())
            {
                return context.Reservations
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.Borrower)
                   .Where(x => x.Book != null && x.IsActive)
                   .OrderBy(x => x.ReservationDate)
                   .ToList();
            }
        }
        public List<Reservation> GetReservationsByBookId(Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                return context.Reservations
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.Borrower)
                   .Where(x => x.Book != null && x.Book.Id == bookId && x.IsActive)
                   .OrderBy(x => x.ReservationDate)
                   .ToList();
            }

        }



    }
}



