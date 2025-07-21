using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;
using OneBeyondApi.Service.Interface;
using System.Collections;

namespace OneBeyondApi.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<Guid> AddAuthorAsync(Author author)
        {
            return await _authorRepository.AddAuthorAsync(author);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _authorRepository.GetAuthorsAsync();
        }
    }
}
