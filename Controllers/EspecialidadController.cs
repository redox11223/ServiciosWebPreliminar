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
        public ActionResult<Especialidad> AgregarEspecialidad(CreateEspecialidadDto especialidad)
        {
            try
            {
                var newEspecialidad = _especialidadService.AgregarEspecialidad(especialidad);
                return CreatedAtAction(nameof(ObtenerEspecialidad), new { id = newEspecialidad.Id }, newEspecialidad);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Especialidad>> ObtenerEspecialidades()
        {
            return Ok(_especialidadService.ObtenerEspecialidades());
        }

        [HttpGet("{id}")]
        public ActionResult<Especialidad> ObtenerEspecialidad(int id)
        {
            try
            {
                return Ok(_especialidadService.ObtenerEspecialidad(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEspecialidad(int id)
        {
            try
            {
                _especialidadService.EliminarEspecialidad(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Especialidad> ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad)
        {
            try
            {
                var updatedEspecialidad = _especialidadService.ActualizarEspecialidad(id, especialidad);
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
