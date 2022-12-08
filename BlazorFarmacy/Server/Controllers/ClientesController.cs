using BlazorFarmacy.Server.Model.Entities;
using BlazorFarmacy.Server.Model;
using BlazorFarmacy.Shared.DTOs.Empleados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorFarmacy.Shared.DTOs.Clientes;

namespace BlazorFarmacy.Server.Controllers
{
    [ApiController, Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ClienteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> GetClientes()
        {
            var clientes = await context.Clientes.ToListAsync();

            var clientesDto = new List<ClienteDTO>();

            foreach (var cliente in clientes)
            {
                var clienteDto = new ClienteDTO();
                clienteDto.Id = cliente.Id;
                clienteDto.Nombre = cliente.Nombre;
                clienteDto.Email = cliente.Email;

                clientesDto.Add(clienteDto);
            }
            return clientesDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDto = new ClienteDTO();
            clienteDto.Id = cliente.Id;
            clienteDto.Nombre = cliente.Nombre;
            clienteDto.Email = cliente.Email;

            return clienteDto;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClienteDTO clienteDto)
        {
            var cliente = new Cliente();
            cliente.Nombre = clienteDto.Nombre;
            cliente.Email = clienteDto.Email;

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] ClienteDTO clienteDto)
        {
            var clienteDb = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == clienteDto.Id);

            if (clienteDb == null)
            {
                return NotFound();
            }

            clienteDb.Nombre = clienteDb.Nombre;
            clienteDb.Email = clienteDb.Email;

            context.Clientes.Update(clienteDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var clienteDb = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (clienteDb == null)
            {
                return NotFound();
            }

            context.Clientes.Remove(clienteDb);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
