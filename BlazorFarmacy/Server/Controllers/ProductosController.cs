using BlazorFarmacy.Server.Model;
using BlazorFarmacy.Server.Model.Entities;
using BlazorFarmacy.Shared.DTOs.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFarmacy.Server.Controllers
{
    [ApiController, Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> GetProductos()
        {
            var productos = await context.Productos.ToListAsync();

            var productosDto = new List<ProductoDTO>();

            foreach (var producto in productos)
            {
                var productoDto = new ProductoDTO();
                productoDto.Id = producto.Id;
                productoDto.Nombre = producto.Nombre;

                productosDto.Add(productoDto);
            }
            return productosDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            var productoDto = new ProductoDTO();
            productoDto.Id = producto.Id;
            productoDto.Nombre = producto.Nombre;

            return productoDto;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductoDTO productoDto)
        {
            var producto = new Producto();
            producto.Nombre = productoDto.Nombre;

            context.Productos.Add(producto);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] ProductoDTO productoDto)
        {
            var productoDb = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == productoDto.Id);

            if (productoDb == null)
            {
                return NotFound();
            }

            productoDb.Nombre = productoDto.Nombre;

            context.Productos.Update(productoDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productoDb = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (productoDb == null)
            {
                return NotFound();
            }

            context.Productos.Remove(productoDb);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

