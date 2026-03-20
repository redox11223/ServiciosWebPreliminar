using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController( IPacienteService pacienteService)
        {
            _pacienteService=pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> ObtenerPacientes()
        {
            return Ok(await _pacienteService.ObtenerPacientes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> ObtenerPaciente(int id)
        {
            try
            {
                return Ok(await _pacienteService.ObtenerPaciente(id));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }



        [HttpPost]
        public async Task<ActionResult<Paciente>> AgregarPaciente(CreatePacienteDto paciente)
        {
            try
            {
                var newPaciente = await _pacienteService.AgregarPaciente(paciente);
                return CreatedAtAction(nameof(ObtenerPaciente), new {id = newPaciente.Id}, newPaciente);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            try
            {
                await _pacienteService.EliminarPaciente(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Paciente>> ActualizarPaciente(int id,CreatePacienteDto paciente)
        {
            try
            {
                var updatedPaciente = await _pacienteService.ActualizarPaciente(id, paciente);
                return Ok(updatedPaciente); 
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
