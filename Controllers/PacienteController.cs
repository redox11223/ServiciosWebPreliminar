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
        public ActionResult<List<Paciente>> ObtenerPacientes()
        {
            return Ok(_pacienteService.ObtenerPacientes());
        }

        [HttpGet("{id}")]
        public ActionResult<Paciente> ObtenerPaciente(int id)
        {
            try
            {
                return Ok(_pacienteService.ObtenerPaciente(id));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }



        [HttpPost]
        public ActionResult<Paciente> AgregarPaciente(CreatePacienteDto paciente)
        {
            try
            {
                var newPaciente = _pacienteService.AgregarPaciente(paciente);
                return CreatedAtAction(nameof(ObtenerPaciente), new {id = newPaciente.Id}, newPaciente);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPaciente(int id)
        {
            try
            {
                _pacienteService.EliminarPaciente(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Paciente> ActualizarPaciente(int id,CreatePacienteDto paciente)
        {
            try
            {
                var updatedPaciente = _pacienteService.ActualizarPaciente(id, paciente);
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
