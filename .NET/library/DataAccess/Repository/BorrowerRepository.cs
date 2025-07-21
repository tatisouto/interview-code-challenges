using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class BorrowerRepository : IBorrowerRepository
    {
        public async Task<IEnumerable<Borrower>> GetBorrowersAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Borrowers
                    .ToListAsync();
            }
        }

        public async Task<Guid> AddBorrowerAsync(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                await context.SaveChangesAsync();
                return borrower.Id;
            }
        }

        public async Task<Borrower?> GetBorrowerByEmailAddressAsync(string emailAddress)
        {
            using (var context = new LibraryContext())
            {
                return await context.Borrowers.FirstOrDefaultAsync(email => email.EmailAddress.Contains(emailAddress));
            }
        }
    }
}
