using SachHayBlog.Core.Domain.Content;
using SachHayBlog.Core.Models;
using SachHayBlog.Core.Models.Content;
using SachHayBlog.Core.SeedWorks;

namespace SachHayBlog.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPostsAsync(int count);
        Task<PageResult<PostInListDto>> GetPostsPagingAsync(string? keyword, Guid? categoryId, int pageIndex = 1, int pageSize = 10);
    }
}
