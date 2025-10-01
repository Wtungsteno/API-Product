using ITS.Day2.BL.Dtos;
using ITS.Day2.Models;
using ITS.Day2.Repositories;

namespace ITS.Day2.BL
{
    public class ProductService(IAppRepository repo) : IProductService
    {
        public IAsyncEnumerable<ProductDto> GetAll()
        {
            return repo.GetAll()
                .Select(ProductToDto);
        }

        public async Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            Product p = await repo.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception($"Product with id {id} not found");

            return ProductToDto(p);
        }

        public async Task<ProductDto> CreateAsync(ProductPostDto dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name is required");
            
            if (dto.Price < 0)
                throw new Exception("Price must be >= 0 or null");

            if (dto.Stock < 0)
                throw new Exception("Stock must be >= 0");

            Product entity = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            repo.Add(entity);
            await repo.SaveAsync(cancellationToken);

            return ProductToDto(entity);
        }

        public async Task UpdateAsync(int id, ProductPatchDto dto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name is required");

            if (dto.Price < 0)
                throw new Exception("Price must be >= 0 or null");

            if (dto.Stock < 0)
                throw new Exception("Stock must be >= 0 or null");

            Product entity = await repo.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception($"Product with id {id} not found");

            if (dto.Name != null)
                entity.Name =  dto.Name;

            entity.Price = dto.Price;
            entity.Stock = dto.Stock ?? entity.Stock;

            await repo.SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Product entity = await repo.GetByIdAsync(id, cancellationToken) 
                ?? throw new Exception($"Product with id {id} not found");

            repo.Delete(entity);
            await repo.SaveAsync(cancellationToken);
        }

        private static ProductDto ProductToDto(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            };
        }
    }
}
