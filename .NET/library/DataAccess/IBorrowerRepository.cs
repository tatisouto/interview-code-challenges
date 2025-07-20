using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<Borrower> GetBorrowers();
        public Borrower? GetBorrowerByEmailAddress(string emailAddress);

        public Guid AddBorrower(Borrower borrower);
    }
}
