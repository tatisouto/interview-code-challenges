using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Service.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Guid> AddAuthorAsync(Author author);
    }
}
