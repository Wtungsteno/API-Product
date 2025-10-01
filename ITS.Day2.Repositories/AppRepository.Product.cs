using ITS.Day2.Models;
using ITS.Day2.Repositories.Services;
using Microsoft.EntityFrameworkCore;

namespace ITS.Day2.Repositories
{
    public partial class AppRepository(AppDbContext db) : IAppRepository
    {
        public IAsyncEnumerable<Product> GetAll()
        {
            return db.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .AsAsyncEnumerable();
        }

        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return db.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }


        public void Add(Product product)
        {
            db.Products.Add(product);
        }

        public void Delete(Product product)
        {
            db.Products.Remove(product);
        }

        public Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return db.SaveChangesAsync(cancellationToken);
        }
    }
}
