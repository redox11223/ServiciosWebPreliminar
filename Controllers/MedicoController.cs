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
        public ActionResult<MedicoResponseDto> AgregarMedico(CreateMedicoDto medico)
        {
            try
            {
                var newMedico = _medicoService.AgregarMedico(medico);
                return CreatedAtAction(nameof(ObtenerMedico), new { id = newMedico.Id }, newMedico);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<MedicoResponseDto>> ObtenerMedicos()
        {
            return Ok(_medicoService.ObtenerMedicos());
        }

        [HttpGet("{id}")]
        public ActionResult<MedicoResponseDto> ObtenerMedico(int id)
        {
            try
            {
                return Ok(_medicoService.ObtenerMedico(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarMedico(int id)
        {
            try
            {
                _medicoService.EliminarMedico(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<MedicoResponseDto> ActualizarMedico(int id, CreateMedicoDto medico)
        {
            try
            {
                var updatedMedico = _medicoService.ActualizarMedico(id, medico);
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