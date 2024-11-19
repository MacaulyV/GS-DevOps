using Microsoft.AspNetCore.Mvc;
using GestaoVeiculos.Models;
using GestaoVeiculos.DTOs.Request;
using GestaoVeiculos.DTOs.Response;
using GestaoVeiculos.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestaoVeiculos.Controllers.API
{
    /// <summary>
    /// Controller responsável pela gestão dos veículos elétricos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoEletricoApiController : ControllerBase
    {
        private readonly IVeiculoEletricoRepository _veiculoEletricoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para inicializar os repositórios de veículos elétricos e usuários.
        /// </summary>
        /// <param name="veiculoEletricoRepository">Repositório de veículos elétricos.</param>
        /// <param name="usuarioRepository">Repositório de usuários.</param>
        public VeiculoEletricoApiController(IVeiculoEletricoRepository veiculoEletricoRepository, IUsuarioRepository usuarioRepository)
        {
            _veiculoEletricoRepository = veiculoEletricoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/VeiculoEletricoApi
        /// <summary>
        /// Retorna todos os veículos elétricos cadastrados.
        /// </summary>
        /// <returns>Lista de veículos elétricos.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/VeiculoEletricoApi
        ///
        /// </remarks>
        /// <response code="200">Retorna a lista de veículos elétricos</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoEletricoResponseDTO>>> GetVeiculosEletricos()
        {
            var veiculos = await _veiculoEletricoRepository.GetAllAsync();

            // Mapeando para a lista de VeiculoEletricoResponseDTO
            var responseList = veiculos.Select(veiculo => new VeiculoEletricoResponseDTO
            {
                ID_Veiculo_Eletrico = veiculo.ID_Veiculo_Eletrico,
                ID_Usuario = veiculo.ID_Usuario,
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Ano = veiculo.Ano,
                Consumo_Medio = veiculo.Consumo_Medio,
                Autonomia = veiculo.Autonomia
            }).ToList();

            return Ok(responseList);
        }

        // POST: api/VeiculoEletricoApi
        /// <summary>
        /// Adiciona um novo veículo elétrico ao sistema.
        /// </summary>
        /// <param name="veiculoDTO">Dados do veículo elétrico a ser adicionado.</param>
        /// <returns>Veículo criado (sem ID).</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/VeiculoEletricoApi
        ///     {
        ///         "id_Usuario": 1,
        ///         "modelo": "Model S",
        ///         "marca": "Tesla",
        ///         "ano": 2021
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o veículo recém-criado</response>
        /// <response code="400">Se os dados fornecidos forem inválidos</response>
        [HttpPost]
        public async Task<ActionResult<VeiculoEletricoResponseDTO>> PostVeiculoEletrico(VeiculoEletricoRequestDTO veiculoDTO)
        {
            // Validar se o usuário existe
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(veiculoDTO.ID_Usuario);
            if (usuarioExistente == null)
            {
                return BadRequest("ID de Usuário não encontrado. Por favor, forneça um ID de usuário válido.");
            }

            // Validar Marca e Modelo
            var veiculoValidacao = await ValidarVeiculo(veiculoDTO.Marca, veiculoDTO.Modelo);
            if (veiculoValidacao == null)
            {
                return BadRequest("Modelo e Marca não disponíveis. Por favor, selecione um veículo válido.");
            }

            // Criar o veículo preenchendo os campos automaticamente
            var veiculo = new VeiculoEletrico
            {
                ID_Usuario = veiculoDTO.ID_Usuario,
                Modelo = veiculoDTO.Modelo,
                Marca = veiculoDTO.Marca,
                Ano = veiculoDTO.Ano,
                Consumo_Medio = veiculoValidacao.Consumo_Medio,
                Autonomia = veiculoValidacao.Autonomia
            };

            await _veiculoEletricoRepository.AddAsync(veiculo);

            // Mapeando para VeiculoEletricoResponseDTO
            var responseDto = new VeiculoEletricoResponseDTO
            {
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Ano = veiculo.Ano,
                Consumo_Medio = veiculo.Consumo_Medio,
                Autonomia = veiculo.Autonomia
            };

            // Configurando opções para evitar $id
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            return new JsonResult(responseDto, jsonOptions)
            {
                StatusCode = 201 // Created
            };
        }

        // GET: api/VeiculoEletricoApi/5
        /// <summary>
        /// Retorna um veículo elétrico específico pelo ID.
        /// </summary>
        /// <param name="id">ID do veículo elétrico.</param>
        /// <returns>Veículo elétrico correspondente.</returns>
        /// <response code="200">Retorna o veículo correspondente</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoEletricoResponseDTO>> GetVeiculoEletrico(int id)
        {
            try
            {
                var veiculo = await _veiculoEletricoRepository.GetByIdAsync(id);
                if (veiculo == null)
                {
                    return NotFound("Veículo não encontrado.");
                }

                var responseDto = new VeiculoEletricoResponseDTO
                {
                    Modelo = veiculo.Modelo,
                    Marca = veiculo.Marca,
                    Ano = veiculo.Ano,
                    Consumo_Medio = veiculo.Consumo_Medio,
                    Autonomia = veiculo.Autonomia
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro interno no servidor.",
                    Detailed = ex.Message
                });
            }
        }

        // PUT: api/VeiculoEletricoApi/5
        /// <summary>
        /// Atualiza os dados de um veículo elétrico específico.
        /// </summary>
        /// <param name="id">ID do veículo elétrico.</param>
        /// <param name="veiculoDTO">Dados atualizados do veículo elétrico.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Quando a atualização for bem-sucedida</response>
        /// <response code="400">Se o ID do veículo ou os dados fornecidos forem inválidos</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculoEletrico(int id, VeiculoEletricoRequestDTO veiculoDTO)
        {
            if (id <= 0)
            {
                return BadRequest("ID do veículo inválido.");
            }

            var veiculoExistente = await _veiculoEletricoRepository.GetByIdAsync(id);
            if (veiculoExistente == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            // Validar se o usuário existe
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(veiculoDTO.ID_Usuario);
            if (usuarioExistente == null)
            {
                return BadRequest("ID de Usuário não encontrado. Por favor, forneça um ID de usuário válido.");
            }

            // Validar Marca e Modelo
            var veiculoValidacao = await ValidarVeiculo(veiculoDTO.Marca, veiculoDTO.Modelo);
            if (veiculoValidacao == null)
            {
                return BadRequest("Modelo e Marca não disponíveis. Por favor, selecione um veículo válido.");
            }

            // Atualizar os campos editáveis
            veiculoExistente.ID_Usuario = veiculoDTO.ID_Usuario;
            veiculoExistente.Modelo = veiculoDTO.Modelo;
            veiculoExistente.Marca = veiculoDTO.Marca;
            veiculoExistente.Ano = veiculoDTO.Ano;

            // Atualizar os campos preenchidos automaticamente
            veiculoExistente.Consumo_Medio = veiculoValidacao.Consumo_Medio;
            veiculoExistente.Autonomia = veiculoValidacao.Autonomia;

            await _veiculoEletricoRepository.UpdateAsync(veiculoExistente);

            return NoContent();
        }

        // DELETE: api/VeiculoEletricoApi/5
        /// <summary>
        /// Exclui um veículo elétrico específico pelo ID.
        /// </summary>
        /// <param name="id">ID do veículo elétrico.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Quando a exclusão for bem-sucedida</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculoEletrico(int id)
        {
            var veiculo = await _veiculoEletricoRepository.GetByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            await _veiculoEletricoRepository.DeleteAsync(id);
            return NoContent();
        }

        // Método auxiliar para validar o veículo e obter os dados adicionais
        /// <summary>
        /// Valida se a marca e o modelo do veículo são válidos e retorna os dados do veículo.
        /// </summary>
        /// <param name="marca">Marca do veículo.</param>
        /// <param name="modelo">Modelo do veículo.</param>
        /// <returns>Dados do veículo elétrico.</returns>
        private async Task<VeiculoEletrico> ValidarVeiculo(string marca, string modelo)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "VeiculosData", "veiculos_eletricos_validacao.json");
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var veiculos = JsonSerializer.Deserialize<List<VeiculoEletrico>>(jsonData, options);

            Console.WriteLine($"Validando Veículo: Marca = '{marca}', Modelo = '{modelo}'");
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine($"Veículo no JSON: Marca = '{veiculo.Marca}', Modelo = '{veiculo.Modelo}'");
            }

            return veiculos.FirstOrDefault(v =>
                v.Marca.Trim().Equals(marca.Trim(), System.StringComparison.OrdinalIgnoreCase) &&
                v.Modelo.Trim().Equals(modelo.Trim(), System.StringComparison.OrdinalIgnoreCase));
        }

    }
}
