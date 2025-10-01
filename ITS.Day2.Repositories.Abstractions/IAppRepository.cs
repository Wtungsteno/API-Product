using ITS.Day2.Models;

namespace ITS.Day2.Repositories
{
    public interface IAppRepository
    {
        IAsyncEnumerable<Product> GetAll();

        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        void Add(Product product);

        void Delete(Product product);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
