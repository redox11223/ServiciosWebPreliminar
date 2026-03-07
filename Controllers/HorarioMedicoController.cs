using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioMedicoController : ControllerBase
    {
        private readonly IHorarioMedicoService _horarioMedicoService;

        public HorarioMedicoController(IHorarioMedicoService horarioMedicoService)
        {
            _horarioMedicoService = horarioMedicoService;
        }

        [HttpPost]
        public ActionResult<HorarioMedicoDto> AgregarHorario(CreateHorarioMedicoDto horario)
        {
            try
            {
                var nuevoHorario = _horarioMedicoService.AgregarHorario(horario);
                return CreatedAtAction(nameof(ObtenerHorarioPorId), new { id = nuevoHorario.Id }, nuevoHorario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<HorarioMedicoDto> ActualizarHorario(int id, CreateHorarioMedicoDto horario)
        {
            try
            {
                var horarioActualizado = _horarioMedicoService.ActualizarHorario(id, horario);
                return Ok(horarioActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarHorario(int id)
        {
            try
            {
                _horarioMedicoService.EliminarHorario(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("medico/{medicoId}")]
        public ActionResult<List<HorarioMedicoDto>> ObtenerHorariosPorMedicoId(int medicoId)
        {
            var horarios = _horarioMedicoService.ObtenerHorarioPorMedicoId(medicoId);
            if (horarios == null || horarios.Count == 0)
                return NotFound($"No se encontraron horarios para el médico.");
            
            return Ok(horarios);
        }

        [HttpGet]
        public ActionResult<List<HorarioMedicoDto>> ListarHorarios()
        {
            var horarios = _horarioMedicoService.ListarHorarios();
            return Ok(horarios);
        }

        [HttpGet("{id}")]
        public ActionResult<HorarioMedicoDto> ObtenerHorarioPorId(int id)
        {
            try
            {
                var horario = _horarioMedicoService.ObtenerHorarioPorId(id);
                return Ok(horario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
