using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Services;

namespace preliminarServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        public async Task<ActionResult<MedicoResponseDto>> AgregarMedico(CreateMedicoDto medico)
        {
            try
            {
                var newMedico = await _medicoService.AgregarMedico(medico);
                return CreatedAtAction(nameof(ObtenerMedico), new { id = newMedico.Id }, newMedico);
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

        [HttpGet]
        public async Task<ActionResult<List<MedicoResponseDto>>> ObtenerMedicos()
        {
            try
            {
                return Ok(await _medicoService.ObtenerMedicos());
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoResponseDto>> ObtenerMedico(int id)
        {
            try
            {
                return Ok(await _medicoService.ObtenerMedico(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMedico(int id)
        {
            try
            {
                await _medicoService.EliminarMedico(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoResponseDto>> ActualizarMedico(int id, CreateMedicoDto medico)
        {
            try
            {
                var updatedMedico = await _medicoService.ActualizarMedico(id, medico);
                return Ok(updatedMedico);
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