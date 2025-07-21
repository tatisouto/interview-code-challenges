using OneBeyondApi.DataAccess.Repository.Interface;
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

        public async Task<Guid> AddFineAsync(Fine fine)
        {
            return await _fineRepository.AddFineAsync(fine);
        }

        public async Task<IEnumerable<Fine>> GetFineAsync()
        {
            return await _fineRepository.GetFineAsync();
        }

        public async Task<Fine?> GetFineBorrowerByIdAsync(Guid borrowerId)
        {
            var result = await _fineRepository.GetFineBorrowerByIdAsync(borrowerId);

            if (result is null)
                throw new InvalidOperationException($"Borrower  '{borrowerId}' not found.");

            return result;
        }

        public async Task<Fine?> GetFineByIdAsync(Guid id)
        {
            var result = await _fineRepository.GetFineByIdAsync(id);

            if (result is null)
                throw new InvalidOperationException($"Fine  '{id}' not found.");

            return result;
        }
    }
}
