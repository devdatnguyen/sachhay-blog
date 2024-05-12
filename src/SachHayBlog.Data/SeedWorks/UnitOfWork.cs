using SachHayBlog.Core.SeedWorks;

namespace SachHayBlog.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SachHayBlogContext _context;

        public UnitOfWork(SachHayBlogContext context)
        {
            _context = context;
        }
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
