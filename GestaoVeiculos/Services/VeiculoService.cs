using GestaoVeiculos.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestaoVeiculos.Services
{
    /// <summary>
    /// Serviço para gerenciar e obter dados sobre veículos, tanto de combustão quanto elétricos, a partir de arquivos JSON.
    /// </summary>
    public class VeiculoService
    {
        private readonly string _combustaoFilePath;
        private readonly string _eletricoFilePath;

        /// <summary>
        /// Construtor do serviço de veículos.
        /// </summary>
        /// <remarks>
        /// Inicializa os caminhos para os arquivos que contêm os dados dos veículos de combustão e elétricos.
        /// </remarks>
        public VeiculoService()
        {
            _combustaoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "veiculos_combustao.json");
            _eletricoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "veiculos_eletricos.json");
        }

        /// <summary>
        /// Obtém a lista de veículos de combustão a partir do arquivo JSON.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="VeiculoModelo"/> que representam veículos de combustão.</returns>
        public async Task<List<VeiculoModelo>> GetVeiculosCombustaoAsync()
        {
            return await GetVeiculosFromFileAsync(_combustaoFilePath);
        }

        /// <summary>
        /// Obtém a lista de veículos elétricos a partir do arquivo JSON.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="VeiculoModelo"/> que representam veículos elétricos.</returns>
        public async Task<List<VeiculoModelo>> GetVeiculosEletricosAsync()
        {
            return await GetVeiculosFromFileAsync(_eletricoFilePath);
        }

        /// <summary>
        /// Método auxiliar para obter a lista de veículos a partir de um arquivo JSON específico.
        /// </summary>
        /// <param name="filePath">Caminho do arquivo JSON.</param>
        /// <returns>Uma lista de objetos <see cref="VeiculoModelo"/> deserializados do arquivo JSON.</returns>
        private async Task<List<VeiculoModelo>> GetVeiculosFromFileAsync(string filePath)
        {
            using (var jsonFileReader = File.OpenText(filePath))
            {
                var content = await jsonFileReader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = new SnakeCaseNamingPolicy() // Utiliza política para converter nomes de propriedades.
                };

                return JsonSerializer.Deserialize<List<VeiculoModelo>>(content, options);
            }
        }
    }
}
