using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface IBorrowerRepository
    {
        Task<IEnumerable<Borrower>> GetBorrowersAsync();
        Task<Borrower?> GetBorrowerByEmailAddressAsync(string emailAddress);
        Task<Guid> AddBorrowerAsync(Borrower borrower);
    }
}
