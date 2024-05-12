namespace SachHayBlog.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAysnc();
    }
}
