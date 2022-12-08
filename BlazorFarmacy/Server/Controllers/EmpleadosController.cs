using BlazorFarmacy.Server.Model;
using BlazorFarmacy.Server.Model.Entities;
using BlazorFarmacy.Shared.DTOs.Empleados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorFarmacy.Server.Controllers
{
    [ApiController, Route("api/empleados")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmpleadosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoDTO>>> GetEmpleados()
        {
            var empleados = await context.Empleados.ToListAsync();

            var empleadosDto = new List<EmpleadoDTO>();

            foreach (var empleado in empleados)
            {
                var empleadoDto = new EmpleadoDTO();
                empleadoDto.Id = empleado.Id;
                empleadoDto.Nombre = empleado.Nombre;
                empleadoDto.Puesto = empleado.Puesto;

                empleadosDto.Add(empleadoDto);
            }
            return empleadosDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadoDTO>> GetAlumno(int id)
        {
            var empleado = await context.Empleados
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            var empleadoDto = new EmpleadoDTO();
            empleadoDto.Id = empleado.Id;
            empleadoDto.Nombre = empleado.Nombre;
            empleadoDto.Puesto = empleado.Puesto;

            return empleadoDto;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EmpleadoDTO empleadoDto)
        {
            var empleado = new Empleado();
            empleado.Nombre = empleadoDto.Nombre;
            empleado.Puesto = empleadoDto.Puesto;

            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] EmpleadoDTO empleadoDto)
        {
            var empleadoDb = await context.Empleados
                .FirstOrDefaultAsync(x => x.Id == empleadoDto.Id);

            if (empleadoDb == null)
            {
                return NotFound();
            }

            empleadoDb.Nombre = empleadoDto.Nombre;
            empleadoDb.Puesto = empleadoDto.Puesto;

            context.Empleados.Update(empleadoDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empleadoDb = await context.Empleados
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empleadoDb == null)
            {
                return NotFound();
            }

            context.Empleados.Remove(empleadoDb);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
