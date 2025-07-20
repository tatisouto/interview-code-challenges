using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;

namespace OneBeyondApi.Service.Interface
{
    public interface IReservationService
    {
        public List<Reservation> GetReservations();
        public Guid ReserveBook(string bookName, string borrowerEmailAddress);
        public ReserveAvailableDto GetReserveAvailable(Guid bookId, Guid borrowerId);
    }


}
