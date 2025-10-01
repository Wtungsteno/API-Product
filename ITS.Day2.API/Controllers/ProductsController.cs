using ITS.Day2.BL;
using ITS.Day2.BL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ITS.Day2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService svc) : ControllerBase
    {
        // GET: api/products
        [HttpGet]
        [SwaggerOperation(Summary = "Returns json data about products")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the products", typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await svc.GetAll().ToArrayAsync(HttpContext.RequestAborted));
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns json data about the requested product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the product with the specified id", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await svc.GetByIdAsync(id, HttpContext.RequestAborted));
        }

        // POST: api/products
        [HttpPost]
        [SwaggerOperation(Summary = "Returns json data about the creted product")]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the created product", typeof(ProductDto))]
        public async Task<IActionResult> Create([FromBody] ProductPostDto dto)
        {
            ProductDto entity = await svc.CreateAsync(dto, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // PATCH: api/products/5
        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "To update an existant product")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Product updated successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductPatchDto dto)
        {
            await svc.UpdateAsync(id, dto, HttpContext.RequestAborted);
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "To delete an existant product")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Product deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> Delete(int id)
        {
            await svc.DeleteAsync(id, HttpContext.RequestAborted);
            return NoContent();
        }
    }
}
