using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GestaoVeiculos.Controllers.API
{
    /// <summary>
    /// Controller responsável por fornecer informações sobre veículos disponíveis para aquisição.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosDisponiveisApiController : ControllerBase
    {
        // GET: api/VeiculosDisponiveisApi/combustao
        /// <summary>
        /// Retorna a lista de veículos de combustão disponíveis.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/VeiculosDisponiveisApi/combustao
        ///
        /// </remarks>
        /// <response code="200">Retorna a lista de veículos de combustão disponíveis</response>
        /// <response code="404">Se o arquivo de veículos de combustão não for encontrado</response>
        [HttpGet("combustao")]
        public async Task<ActionResult<IEnumerable<object>>> GetVeiculosCombustaoDisponiveis()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "VeiculosData", "veiculos_combustao_disponiveis.json");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Arquivo de veículos de combustão disponíveis não encontrado.");
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var veiculos = JsonSerializer.Deserialize<List<object>>(jsonData, options);
            return Ok(veiculos);
        }

        // GET: api/VeiculosDisponiveisApi/eletricos
        /// <summary>
        /// Retorna a lista de veículos elétricos disponíveis.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/VeiculosDisponiveisApi/eletricos
        ///
        /// </remarks>
        /// <response code="200">Retorna a lista de veículos elétricos disponíveis</response>
        /// <response code="404">Se o arquivo de veículos elétricos não for encontrado</response>
        [HttpGet("eletricos")]
        public async Task<ActionResult<IEnumerable<object>>> GetVeiculosEletricosDisponiveis()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "VeiculosData", "veiculos_eletricos_disponiveis.json");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Arquivo de veículos elétricos disponíveis não encontrado.");
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var veiculos = JsonSerializer.Deserialize<List<object>>(jsonData, options);
            return Ok(veiculos);
        }
    }
}
