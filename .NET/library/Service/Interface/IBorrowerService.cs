using OneBeyondApi.Model;

namespace OneBeyondApi.Service.Interface
{
    public interface IBorrowerService
    {
        Task<IEnumerable<Borrower>> GetBorrowersAsync();
        Task<Borrower?> GetBorrowerByEmailAddressAsync(string emailAddress);
        Task<Guid> AddBorrowerAsync(Borrower borrower);
    }
}
