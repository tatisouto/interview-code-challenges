using OneBeyondApi.Model;
using OneBeyondApi.Model.Dto;

namespace OneBeyondApi.Service.Interface
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Guid> ReserveBookAsync(string bookName, string borrowerEmailAddress);
        Task<ReserveAvailableDto> GetReserveAvailableAsync(Guid bookId, Guid borrowerId);
    }


}
