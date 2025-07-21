using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class FineRepository : IFineRepository
    {

        public async Task<IEnumerable<Fine>> GetFineAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Fines
                    .Include(x => x.Borrower)
                    .ToListAsync();
            }
        }

        public async Task<Fine?> GetFineByIdAsync(Guid id)
        {
            using (var context = new LibraryContext())
            {
                return await context.Fines
                    .Include(x => x.Borrower)
                    .FirstOrDefaultAsync(x => x.Id == id);

            }
        }


        public async Task<Fine?> GetFineBorrowerByIdAsync(Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                return await context.Fines
                    .Include(x => x.Borrower)
                    .FirstOrDefaultAsync(x => x.Borrower.Id == borrowerId);
            }
        }

        public async Task<Guid> AddFineAsync(Fine fine)
        {
            using (var context = new LibraryContext())
            {

                context.Attach(fine.Borrower);

                context.Fines.Add(fine);
                await context.SaveChangesAsync();
                return fine.Id;
            }
        }
    }
}
