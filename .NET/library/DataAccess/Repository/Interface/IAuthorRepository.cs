using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository.Interface
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Guid> AddAuthorAsync(Author author);
    }
}
