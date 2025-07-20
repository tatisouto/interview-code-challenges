using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class FineRepository : IFineRepository
    {
        public FineRepository()
        {

        }

        public List<Fine> GetFine()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Fines
                    .Include(x => x.Borrower)
                    .ToList();
                return list;
            }
        }

        public Fine? GetFineById(Guid id)
        {
            using (var context = new LibraryContext())
            {
                return context.Fines
                    .Include(x => x.Borrower)
                    .FirstOrDefault(x => x.Id == id);

            }
        }


        public Fine? GetFineBorrowerById(Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                return context.Fines
                    .Include(x => x.Borrower)
                    .FirstOrDefault(x => x.Borrower.Id == borrowerId);

            }
        }


        public Guid AddFines(Fine fine)
        {
            using (var context = new LibraryContext())
            {

                context.Attach(fine.Borrower);

                context.Fines.Add(fine);
                context.SaveChanges();
                return fine.Id;
            }
        }
    }
}
