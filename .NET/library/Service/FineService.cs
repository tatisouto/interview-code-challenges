using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;

namespace OneBeyondApi.Service
{
    public class FineService : IFineService
    {

        private readonly IFineRepository _fineRepository;

        public FineService(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        public Guid AddFines(Fine fine)
        {
            return _fineRepository.AddFines(fine);
        }

        public List<Fine> GetFine()
        {
            return _fineRepository.GetFine();
        }

        public Fine? GetFineBorrowerById(Guid borrowerId)
        {
            var result = _fineRepository.GetFineBorrowerById(borrowerId);

            if (result is null)
                throw new InvalidOperationException($"Borrower  '{borrowerId}' not found.");

            return result;
        }

        public Fine? GetFineById(Guid id)
        {
            var result = _fineRepository.GetFineById(id);

            if (result is null)
                throw new InvalidOperationException($"Fine  '{id}' not found.");

            return result;
        }
    }
}
