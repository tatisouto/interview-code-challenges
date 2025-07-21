using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess.Context;
using OneBeyondApi.DataAccess.Repository.Interface;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public AuthorRepository() { }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            using (var context = new LibraryContext())
            {
                return await context.Authors
                    .ToListAsync();
            }
        }

        public async Task<Guid> AddAuthorAsync(Author author)
        {
            using (var context = new LibraryContext())
            {
                context.Authors.Add(author);
                await context.SaveChangesAsync();
                return author.Id;
            }
        }
    }
}
