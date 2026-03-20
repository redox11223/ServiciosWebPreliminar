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
        public async Task<ActionResult<HorarioMedicoDto>> AgregarHorario(CreateHorarioMedicoDto horario)
        {
            try
            {
                var nuevoHorario = await _horarioMedicoService.AgregarHorario(horario);
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
        public async Task<ActionResult<HorarioMedicoDto>> ActualizarHorario(int id, CreateHorarioMedicoDto horario)
        {
            try
            {
                var horarioActualizado = await _horarioMedicoService.ActualizarHorario(id, horario);
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
        public async Task<IActionResult> EliminarHorario(int id)
        {
            try
            {
                await _horarioMedicoService.EliminarHorario(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("medico/{medicoId}")]
        public async Task<ActionResult<List<HorarioMedicoDto>>> ObtenerHorariosPorMedicoId(int medicoId)
        {
            try
            {
                var horarios = await _horarioMedicoService.ObtenerHorariosPorMedicoId(medicoId);
                if (!horarios.Any())
                    return NotFound($"No se encontraron horarios para el médico.");
                return Ok(horarios);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<HorarioMedicoDto>>> ListarHorarios()
        {
            try
            {
                var horarios = await _horarioMedicoService.ListarHorarios();
                return Ok(horarios);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioMedicoDto>> ObtenerHorarioPorId(int id)
        {
            try
            {
                var horario = await _horarioMedicoService.ObtenerHorarioPorId(id);
                return Ok(horario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
