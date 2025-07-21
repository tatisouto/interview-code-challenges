using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        public ReservationRepository() { }

        public async Task<Guid> AddReservationAsync(Reservation reservation)
        {
            using (var context = new LibraryContext())
            {
                context.Attach(reservation.Borrower);
                context.Attach(reservation.Book);

                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();
                return reservation.Id;
            }
        }
        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Reservations
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.Borrower)
                   .Where(x => x.Book != null && x.IsActive)
                   .OrderBy(x => x.ReservationDate)
                   .ToListAsync();
            }
        }
        public async Task<IEnumerable<Reservation>> GetReservationsByBookIdAsync(Guid bookId)
        {
            using (var context = new LibraryContext())
            {
                return await context.Reservations
                   .Include(x => x.Book)
                   .ThenInclude(x => x.Author)
                   .Include(x => x.Borrower)
                   .Where(x => x.Book != null && x.Book.Id == bookId && x.IsActive)
                   .OrderBy(x => x.ReservationDate)
                   .ToListAsync();
            }

        }



    }
}



