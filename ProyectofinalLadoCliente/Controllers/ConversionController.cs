using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectofinalLadoCliente.Models.ViewModels;
using ProyectofinalLadoCliente.Services;

namespace ProyectofinalLadoCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly ConversionService _conversionService;

        public ConversionController()
        {
            _conversionService = new ConversionService();
        }

      
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API Conversion está activa");
        }


        [HttpPost]
        public IActionResult ProcesarConversion(ConversionDTO model)
        {
            if (model == null)
                return BadRequest("Modelo vacío");

            if (string.IsNullOrWhiteSpace(model.NumeroOriginal))
                return BadRequest("Debe ingresar un número.");

            if (model.BaseOrigen < 2 || model.BaseOrigen > 36 ||
                model.BaseDestino < 2 || model.BaseDestino > 36)
            {
                return BadRequest("Las bases deben estar entre 2 y 36.");
            }

            try
            {
                var resultado = _conversionService.Convertir(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error procesando la conversión: {ex.Message}");
            }
        }

    }
}
