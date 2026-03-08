using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController(CitaService citaService) : ControllerBase
    { 
        private readonly CitaService _citaService = citaService;

        [HttpPost]
        public ActionResult<CitaDto> AgregarCita([FromBody] CreateCitaDto cita)
        {
            try
            {
                var citaDto = _citaService.AgregarCita(cita);
                return CreatedAtAction(nameof(ObtenerCita), new { id = citaDto.Id }, citaDto);
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

        [HttpGet("{id}")]
        public ActionResult<CitaDto> ObtenerCita(int id)
        {
            try
            {
                var citaDto = _citaService.ObtenerCita(id);
                return Ok(citaDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<CitaDto>> ObtenerCitas()
        {
            try
            {
                return Ok(_citaService.ObtenerCitas());
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CitaDto> ActualizarCita(int id, [FromBody] CreateCitaDto cita)
        {
            try
            {
                var citaDto = _citaService.ActualizarCita(id, cita);
                return Ok(citaDto);
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
        public IActionResult EliminarCita(int id)
        {
            try
            {
                _citaService.EliminarCita(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
