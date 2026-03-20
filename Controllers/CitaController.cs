using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController(ICitaService citaService) : ControllerBase
    { 
        private readonly ICitaService _citaService = citaService;

        [HttpPost]
        public async Task<ActionResult<CitaDto>> AgregarCita([FromBody] CreateCitaDto cita)
        {
            try
            {
                var citaDto = await _citaService.AgregarCita(cita);
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
        public async Task<ActionResult<CitaDto>> ObtenerCita(int id)
        {
            try
            {
                var citaDto = await _citaService.ObtenerCita(id);
                return Ok(citaDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> ObtenerCitas()
        {
            try
            {
                return Ok(await _citaService.ObtenerCitas());
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CitaDto>> ActualizarCita(int id, [FromBody] CreateCitaDto cita)
        {
            try
            {
                var citaDto = await _citaService.ActualizarCita(id, cita);
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
        public async Task<IActionResult> EliminarCita(int id)
        {
            try
            {
                await _citaService.EliminarCita(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
