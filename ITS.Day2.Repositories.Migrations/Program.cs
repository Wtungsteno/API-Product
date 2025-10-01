using Microsoft.EntityFrameworkCore;

namespace ITS.Day2.Repositories.Migrations
{
    internal static class Program
    {
        private static Task Main()
        {
            return new AppDbContextFactory().CreateDbContext(Array.Empty<string>()).Database.MigrateAsync();
        }
    }
}
