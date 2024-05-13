namespace SachHayBlog.Core.Models
{
    public class PageResult<T> : PgaeResultBase where T : class
    {
        public List<T> Results { get; set; }

        public PageResult() 
        {
            Results = new List<T>();
        }
    }
}
