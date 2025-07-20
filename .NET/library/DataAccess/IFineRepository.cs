using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IFineRepository
    {
        public List<Fine> GetFine();
        public Fine? GetFineById(Guid id);
        public Guid AddFines(Fine fine);
    }
}
