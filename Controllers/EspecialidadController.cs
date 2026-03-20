using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpPost]
        public async Task<ActionResult<Especialidad>> AgregarEspecialidad(CreateEspecialidadDto especialidad)
        {
            try
            {
                var newEspecialidad = await _especialidadService.AgregarEspecialidad(especialidad);
                return CreatedAtAction(nameof(ObtenerEspecialidad), new { id = newEspecialidad.Id }, newEspecialidad);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Especialidad>>> ObtenerEspecialidades()
        {
            return Ok(await _especialidadService.ObtenerEspecialidades());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidad>> ObtenerEspecialidad(int id)
        {
            try
            {
                return Ok(await _especialidadService.ObtenerEspecialidad(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEspecialidad(int id)
        {
            try
            {
                await _especialidadService.EliminarEspecialidad(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Especialidad>> ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad)
        {
            try
            {
                var updatedEspecialidad = await _especialidadService.ActualizarEspecialidad(id, especialidad);
                return Ok(updatedEspecialidad);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
