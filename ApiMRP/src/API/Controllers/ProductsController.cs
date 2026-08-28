using ApiMRP.Domain;
using ApiMRP.Dtos;
using ApiMRP.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMRP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductDto dto)
    {
        if (dto.Price < 0 || dto.Stock < 0)
        {
            return BadRequest("El precio y el stock deben ser mayores o iguales a 0.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var response = MapToDto(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products.Select(MapToDto));
    }

    // GET: api/products/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductById(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound($"Producto con ID {id} no encontrado.");
        }

        return Ok(MapToDto(product));
    }

    // PUT: api/products/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound($"Producto con ID {id} no encontrado.");
        }

        if (dto.Price < 0 || dto.Stock < 0)
        {
            return BadRequest("El precio y el stock deben ser mayores o iguales a 0.");
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound($"Producto con ID {id} no encontrado.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static ProductResponseDto MapToDto(Product product) =>
        new(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CreatedAt);
}