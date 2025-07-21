using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface IFineRepository
    {
        Task<IEnumerable<Fine>> GetFineAsync();
        Task<Fine?> GetFineByIdAsync(Guid id);
        Task<Guid> AddFineAsync(Fine fine);
        Task<Fine?> GetFineBorrowerByIdAsync(Guid borrowerId);
    }
}
