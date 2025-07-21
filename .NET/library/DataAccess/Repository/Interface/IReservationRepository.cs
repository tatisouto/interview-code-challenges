using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Guid> AddReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsByBookIdAsync(Guid bookId);

    }
}
