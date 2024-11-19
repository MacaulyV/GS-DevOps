using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GestaoVeiculos.Models;
using GestaoVeiculos.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GestaoVeiculos.Controllers.API
{
    /// <summary>
    /// Controller responsável pela comparação de eficiência entre veículos de combustão e veículos elétricos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ComparacaoEficienciaApiController : ControllerBase
    {
        private readonly IVeiculoCombustaoRepository _veiculoCombustaoRepository;
        private readonly IVeiculoEletricoRepository _veiculoEletricoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para inicializar os repositórios de veículos de combustão, elétricos e de usuários.
        /// </summary>
        /// <param name="veiculoCombustaoRepository">Repositório de veículos de combustão.</param>
        /// <param name="veiculoEletricoRepository">Repositório de veículos elétricos.</param>
        /// <param name="usuarioRepository">Repositório de usuários.</param>
        public ComparacaoEficienciaApiController(
            IVeiculoCombustaoRepository veiculoCombustaoRepository,
            IVeiculoEletricoRepository veiculoEletricoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _veiculoCombustaoRepository = veiculoCombustaoRepository;
            _veiculoEletricoRepository = veiculoEletricoRepository;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Compara a eficiência entre um veículo de combustão e um veículo elétrico para um determinado usuário.
        /// </summary>
        /// <param name="idUsuario">ID do usuário dono dos veículos.</param>
        /// <param name="idVeiculoCombustao">ID do veículo de combustão.</param>
        /// <param name="idVeiculoEletrico">ID do veículo elétrico.</param>
        /// <returns>Resultado da comparação de eficiência, incluindo análise detalhada e conclusão.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/ComparacaoEficienciaApi/Comparar
        ///     {
        ///         "idUsuario": 1,
        ///         "idVeiculoCombustao": 101,
        ///         "idVeiculoEletrico": 202
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o resultado da comparação de eficiência</response>
        /// <response code="400">Se algum dos IDs fornecidos não for encontrado ou não pertencer ao usuário</response>
        [HttpPost("Comparar")]
        public async Task<ActionResult<string>> CompararEficiencia(int idUsuario, int idVeiculoCombustao, int idVeiculoEletrico)
        {
            // Verificar se o usuário existe
            var usuario = await _usuarioRepository.GetByIdAsync(idUsuario);
            if (usuario == null)
            {
                return BadRequest("ID de Usuário não encontrado. Por favor, forneça um ID de usuário válido.");
            }

            // Verificar se o veículo de combustão existe e pertence ao usuário
            var veiculoCombustao = await _veiculoCombustaoRepository.GetByIdAsync(idVeiculoCombustao);
            if (veiculoCombustao == null || veiculoCombustao.ID_Usuario != idUsuario)
            {
                return BadRequest("Veículo de combustão não encontrado ou não pertence ao usuário fornecido.");
            }

            // Verificar se o veículo elétrico existe e pertence ao usuário
            var veiculoEletrico = await _veiculoEletricoRepository.GetByIdAsync(idVeiculoEletrico);
            if (veiculoEletrico == null || veiculoEletrico.ID_Usuario != idUsuario)
            {
                return BadRequest("Veículo elétrico não encontrado ou não pertence ao usuário fornecido.");
            }

            // Usar a quilometragem mensal do veículo de combustão
            double quilometragemMensal = veiculoCombustao.Quilometragem_Mensal;

            // Calcular a eficiência com arredondamento para uma casa decimal
            double litrosNecessarios = Math.Round(quilometragemMensal / veiculoCombustao.Consumo_Medio, 1);
            double tanquesNecessarios = Math.Round(litrosNecessarios / veiculoCombustao.Autonomia_Tanque, 1);
            double cargasNecessarias = Math.Round(quilometragemMensal / veiculoEletrico.Autonomia, 1);

            // Análise detalhada dos resultados
            string analiseDetalhada = $"Para o seu veículo de combustão, com uma quilometragem mensal de {quilometragemMensal} km " +
                                      $"e um consumo médio de {veiculoCombustao.Consumo_Medio} km/l, você precisará de aproximadamente {litrosNecessarios} litros de combustível por mês, " +
                                      $"o que corresponde a cerca de {tanquesNecessarios} tanques cheios (considerando a autonomia total do tanque de {veiculoCombustao.Autonomia_Tanque} litros). " +
                                      $"Já para o veículo elétrico, com a mesma quilometragem mensal de {quilometragemMensal} km e uma autonomia de {veiculoEletrico.Autonomia} km por carga, " +
                                      $"você precisará de aproximadamente {cargasNecessarias} cargas completas de bateria por mês.";

            // Montar a conclusão, destacando as vantagens de longo prazo do veículo elétrico
            string conclusao = "Embora o custo inicial de aquisição do veículo elétrico possa ser maior, ele se mostra mais eficiente e econômico a longo prazo. " +
                               $"Com base nos dados fornecidos, você precisará de aproximadamente {tanquesNecessarios} tanques de combustível para o veículo de combustão e {cargasNecessarias} cargas completas para o veículo elétrico por mês. " +
                               $"A economia gerada ao longo do tempo em combustível, além da redução das emissões de carbono, pode compensar significativamente o investimento inicial na transição para um veículo elétrico.";

            // Montar a resposta em JSON
            var resultado = new
            {
                veiculoCombustao = new
                {
                    modelo = veiculoCombustao.Modelo,
                    quilometragemMensal = quilometragemMensal,
                    consumoMedio = veiculoCombustao.Consumo_Medio,
                    litrosNecessarios = litrosNecessarios,
                    tanquesNecessarios = tanquesNecessarios
                },
                veiculoEletrico = new
                {
                    modelo = veiculoEletrico.Modelo,
                    quilometragemMensal = quilometragemMensal,
                    autonomia = veiculoEletrico.Autonomia,
                    cargasNecessarias = cargasNecessarias
                },
                analiseDetalhada = analiseDetalhada,
                conclusao = conclusao
            };

            // Retornar a resposta
            return Ok(resultado);
        }
    }
}
