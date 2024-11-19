using Microsoft.AspNetCore.Mvc;
using GestaoVeiculos.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using GestaoVeiculos.Repositories.Interfaces;
using System.IO;
using System.Text.Json;
using GestaoVeiculos.DTOs.Request;
using System;
using GestaoVeiculos.DTOs.Response;

namespace GestaoVeiculos.Controllers.API
{
    /// <summary>
    /// Controller responsável pela gestão dos veículos de combustão.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoCombustaoApiController : ControllerBase
    {
        private readonly IVeiculoCombustaoRepository _veiculoCombustaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para inicializar os repositórios de veículos de combustão e usuários.
        /// </summary>
        /// <param name="veiculoCombustaoRepository">Repositório de veículos de combustão.</param>
        /// <param name="usuarioRepository">Repositório de usuários.</param>
        public VeiculoCombustaoApiController(IVeiculoCombustaoRepository veiculoCombustaoRepository, IUsuarioRepository usuarioRepository)
        {
            _veiculoCombustaoRepository = veiculoCombustaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/VeiculoCombustaoApi
        /// <summary>
        /// Retorna todos os veículos de combustão cadastrados.
        /// </summary>
        /// <returns>Lista de veículos de combustão.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/VeiculoCombustaoApi
        ///
        /// </remarks>
        /// <response code="200">Retorna a lista de veículos de combustão</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoCombustao>>> GetVeiculosCombustao()
        {
            var veiculos = await _veiculoCombustaoRepository.GetAllAsync();
            if (veiculos == null || !veiculos.Any())
            {
                return NotFound("Nenhum veículo encontrado.");
            }

            return Ok(veiculos.Select(v => new VeiculoCombustaoResponseDTO
            {
                ID_Veiculo_Combustao = v.ID_Veiculo_Combustao,
                ID_Usuario = v.ID_Usuario,
                Modelo = v.Modelo,
                Marca = v.Marca,
                Ano = v.Ano,
                Quilometragem_Mensal = v.Quilometragem_Mensal,
                Consumo_Medio = v.Consumo_Medio,
                Autonomia_Tanque = v.Autonomia_Tanque,
                Tipo_Combustivel = v.Tipo_Combustivel
            }));
        }


        // POST: api/VeiculoCombustaoApi
        /// <summary>
        /// Adiciona um novo veículo de combustão ao sistema.
        /// </summary>
        /// <param name="veiculoDTO">Dados do veículo de combustão a ser adicionado.</param>
        /// <returns>Veículo criado (sem ID).</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/VeiculoCombustaoApi
        ///     {
        ///         "id_Usuario": 1,
        ///         "modelo": "Civic",
        ///         "marca": "Honda",
        ///         "ano": 2020,
        ///         "quilometragem_Mensal": 1500
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o veículo recém-criado</response>
        /// <response code="400">Se os dados fornecidos forem inválidos</response>
        /// <response code="500">Se ocorrer um erro no servidor</response>
        [HttpPost]
        public async Task<ActionResult<VeiculoCombustaoResponseDTO>> PostVeiculoCombustao(VeiculoCombustaoRequestDTO veiculoDTO)
        {
            try
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
                var veiculo = new VeiculoCombustao
                {
                    ID_Usuario = veiculoDTO.ID_Usuario,
                    Modelo = veiculoDTO.Modelo,
                    Marca = veiculoDTO.Marca,
                    Ano = veiculoDTO.Ano,
                    Quilometragem_Mensal = veiculoDTO.Quilometragem_Mensal,
                    Consumo_Medio = veiculoValidacao.Consumo_Medio,
                    Autonomia_Tanque = veiculoValidacao.Autonomia_Tanque,
                    Tipo_Combustivel = veiculoValidacao.Tipo_Combustivel
                };

                await _veiculoCombustaoRepository.AddAsync(veiculo);

                // Mapeando o veículo para o DTO de resposta
                var responseDto = new VeiculoCombustaoResponseDTO
                {
                    Modelo = veiculo.Modelo,
                    Marca = veiculo.Marca,
                    Ano = veiculo.Ano,
                    Quilometragem_Mensal = (int)veiculo.Quilometragem_Mensal,
                    Consumo_Medio = veiculo.Consumo_Medio,
                    Autonomia_Tanque = (int)veiculo.Autonomia_Tanque,
                    Tipo_Combustivel = veiculo.Tipo_Combustivel
                };

                // Retornar o DTO sem IDs
                return CreatedAtAction(nameof(GetVeiculoCombustao), new { id = veiculo.ID_Veiculo_Combustao }, responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro interno no servidor.",
                    Detailed = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                });
            }
        }

        // GET: api/VeiculoCombustaoApi/5
        /// <summary>
        /// Retorna um veículo de combustão específico pelo ID.
        /// </summary>
        /// <param name="id">ID do veículo de combustão.</param>
        /// <returns>Veículo de combustão correspondente.</returns>
        /// <response code="200">Retorna o veículo correspondente</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoCombustaoResponseDTO>> GetVeiculoCombustao(int id)
        {
            var veiculo = await _veiculoCombustaoRepository.GetByIdAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            // Mapeando para VeiculoCombustaoResponseDTO
            var responseDto = new VeiculoCombustaoResponseDTO
            {
                Modelo = veiculo.Modelo,
                Marca = veiculo.Marca,
                Ano = veiculo.Ano,
                Consumo_Medio = veiculo.Consumo_Medio,
                Autonomia_Tanque = (int)veiculo.Autonomia_Tanque
            };

            return Ok(responseDto);
        }

        // PUT: api/VeiculoCombustaoApi/5
        /// <summary>
        /// Atualiza os dados de um veículo de combustão específico.
        /// </summary>
        /// <param name="id">ID do veículo de combustão.</param>
        /// <param name="veiculoDTO">Dados atualizados do veículo de combustão.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="200">Retorna os dados atualizados do veículo</response>
        /// <response code="400">Se o ID do veículo ou os dados fornecidos forem inválidos</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        /// <response code="500">Se ocorrer um erro no servidor</response>
        
        // PUT: api/VeiculoCombustaoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculoCombustao(int id, VeiculoCombustaoRequestDTO veiculoDTO)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID do veículo inválido.");
                }

                // Verificar se o veículo de combustão existe
                var veiculoExistente = await _veiculoCombustaoRepository.GetByIdAsync(id);
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
                veiculoExistente.Quilometragem_Mensal = veiculoDTO.Quilometragem_Mensal;

                // Atualizar os campos preenchidos automaticamente
                veiculoExistente.Consumo_Medio = veiculoValidacao.Consumo_Medio;
                veiculoExistente.Autonomia_Tanque = veiculoValidacao.Autonomia_Tanque;
                veiculoExistente.Tipo_Combustivel = veiculoValidacao.Tipo_Combustivel;

                // Atualizar no repositório
                await _veiculoCombustaoRepository.UpdateAsync(veiculoExistente);

                // Retornar a confirmação da atualização com os dados necessários
                var responseDto = new VeiculoCombustaoResponseDTO
                {
                    ID_Veiculo_Combustao = veiculoExistente.ID_Veiculo_Combustao,
                    ID_Usuario = veiculoExistente.ID_Usuario,
                    Modelo = veiculoExistente.Modelo,
                    Marca = veiculoExistente.Marca,
                    Ano = veiculoExistente.Ano,
                    Quilometragem_Mensal = veiculoExistente.Quilometragem_Mensal,
                    Consumo_Medio = veiculoExistente.Consumo_Medio,
                    Autonomia_Tanque = veiculoExistente.Autonomia_Tanque,
                    Tipo_Combustivel = veiculoExistente.Tipo_Combustivel
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro interno no servidor.",
                    Detailed = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                });
            }
        }

        // DELETE: api/VeiculoCombustaoApi/5
        /// <summary>
        /// Exclui um veículo de combustão específico pelo ID.
        /// </summary>
        /// <param name="id">ID do veículo de combustão.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Quando a exclusão for bem-sucedida</response>
        /// <response code="404">Se o veículo não for encontrado</response>
        /// <response code="500">Se ocorrer um erro no servidor</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculoCombustao(int id)
        {
            try
            {
                var veiculo = await _veiculoCombustaoRepository.GetByIdAsync(id);
                if (veiculo == null)
                {
                    return NotFound("Veículo não encontrado.");
                }

                await _veiculoCombustaoRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro interno no servidor.",
                    Detailed = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                });
            }
        }

        // Método auxiliar para validar o veículo e obter os dados adicionais
        /// <summary>
        /// Valida se a marca e o modelo do veículo são válidos e retorna os dados do veículo.
        /// </summary>
        /// <param name="marca">Marca do veículo.</param>
        /// <param name="modelo">Modelo do veículo.</param>
        /// <returns>Dados do veículo de combustão.</returns>
        private async Task<VeiculoCombustao> ValidarVeiculo(string marca, string modelo)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "VeiculosData", "veiculos_combustao_validacao.json");
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }

            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var veiculos = JsonSerializer.Deserialize<List<VeiculoCombustao>>(jsonData, options);

            return veiculos.FirstOrDefault(v =>
                v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase) &&
                v.Modelo.Equals(modelo, StringComparison.OrdinalIgnoreCase));
        }
    }
}
