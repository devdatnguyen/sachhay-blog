using AutoMapper;
using SachHayBlog.Core.Repositories;
using SachHayBlog.Core.SeedWorks;
using SachHayBlog.Data.Repositories;

namespace SachHayBlog.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SachHayBlogContext _context;

        public UnitOfWork(SachHayBlogContext context, IMapper mapper)
        {
            _context = context;
            Posts = new PostRepository(context, mapper);
        }

        public IPostRepository Posts { get; private set;}
        public async Task<int> CompleteAysnc()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
