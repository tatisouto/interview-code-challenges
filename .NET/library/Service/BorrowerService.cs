using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;
using System.Net.Mail;

namespace OneBeyondApi.Service
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }
        public async Task<Guid> AddBorrowerAsync(Borrower borrower)
        {
            return await _borrowerRepository.AddBorrowerAsync(borrower);
        }

        public async Task<Borrower?> GetBorrowerByEmailAddressAsync(string emailAddress)
        {
            return await _borrowerRepository.GetBorrowerByEmailAddressAsync(emailAddress);
        }

        public async Task<IEnumerable<Borrower>> GetBorrowersAsync()
        {
            return await _borrowerRepository.GetBorrowersAsync();
        }
    }
}
