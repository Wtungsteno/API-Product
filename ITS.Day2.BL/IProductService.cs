using ITS.Day2.BL.Dtos;

namespace ITS.Day2.BL
{
    public interface IProductService
    {
        IAsyncEnumerable<ProductDto> GetAll();

        Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<ProductDto> CreateAsync(ProductPostDto dto, CancellationToken cancellationToken = default);

        Task UpdateAsync(int id, ProductPatchDto dto, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
