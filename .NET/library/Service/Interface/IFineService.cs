using OneBeyondApi.Model;

namespace OneBeyondApi.Service.Interface
{
    public interface IFineService
    {
        public List<Fine> GetFine();
        public Fine? GetFineById(Guid id);
        public Guid AddFines(Fine fine);
        public Fine? GetFineBorrowerById(Guid borrowerId);
    }

}
