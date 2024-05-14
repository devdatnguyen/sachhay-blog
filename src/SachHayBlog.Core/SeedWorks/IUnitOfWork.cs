using SachHayBlog.Core.Repositories;

namespace SachHayBlog.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        Task<int> CompleteAysnc();
    }
}
