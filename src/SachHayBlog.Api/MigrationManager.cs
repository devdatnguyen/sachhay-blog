using Microsoft.EntityFrameworkCore;
using SachHayBlog.Data;

namespace SachHayBlog.Api
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope()) { 
                using (var context = scope.ServiceProvider.GetRequiredService<SachHayBlogContext>())
                {
                    context.Database.Migrate();
                    new DataSeeder().SeedAsync(context).Wait();
                }
            } 
            return app;
        }
    }
}
