using Microsoft.EntityFrameworkCore;
using SachHayBlog.Core.Domain.Content;
using SachHayBlog.Core.Repositories;
using SachHayBlog.Data.SeedWorks;

namespace SachHayBlog.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post, Guid>, IPostRepository
    {
        public PostRepository(SachHayBlogContext context) : base(context)
        {
        }

        public Task<List<Post>> GetPopularPostAsync(int count)
        {
            return _context.Posts.OrderByDescending(x => x.ViewCount).Take(count).ToListAsync();
        }
    }
}
