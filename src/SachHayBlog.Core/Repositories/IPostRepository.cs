using SachHayBlog.Core.Domain.Content;
using SachHayBlog.Core.SeedWorks;

namespace SachHayBlog.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPostAsync(int count);
    }
}
