using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IReservationRepository
    {
        public List<Reservation> GetReservations();

        public Guid AddReservation(Reservation reservation);

        List<Reservation> GetReservationsByBookId(Guid bookId);
        
    }
}
