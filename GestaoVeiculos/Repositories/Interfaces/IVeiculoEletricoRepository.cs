using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Models;

namespace GestaoVeiculos.Repositories.Interfaces
{
    /// <summary>
    /// Interface para o repositório de veículos elétricos.
    /// Define os métodos necessários para o gerenciamento dos dados de veículos elétricos,
    /// proporcionando uma camada de abstração para operações CRUD e integração com a fonte de dados.
    /// </summary>
    public interface IVeiculoEletricoRepository
    {
        /// <summary>
        /// Obtém todos os veículos elétricos cadastrados no sistema.
        /// </summary>
        /// <returns>Uma lista assíncrona de objetos <see cref="VeiculoEletrico"/> representando todos os veículos elétricos disponíveis.</returns>
        Task<IEnumerable<VeiculoEletrico>> GetAllAsync();

        /// <summary>
        /// Obtém um veículo elétrico pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo elétrico que será pesquisado.</param>
        /// <returns>Um objeto <see cref="VeiculoEletrico"/> se encontrado, ou <c>null</c> se o veículo não existir.</returns>
        Task<VeiculoEletrico> GetByIdAsync(int id);

        /// <summary>
        /// Adiciona um novo veículo elétrico ao sistema.
        /// </summary>
        /// <param name="veiculo">Dados do veículo elétrico a ser adicionado ao banco de dados.</param>
        Task AddAsync(VeiculoEletrico veiculo);

        /// <summary>
        /// Atualiza os dados de um veículo elétrico existente.
        /// </summary>
        /// <param name="veiculo">Dados atualizados do veículo elétrico que serão persistidos no banco de dados.</param>
        Task UpdateAsync(VeiculoEletrico veiculo);

        /// <summary>
        /// Remove um veículo elétrico do sistema com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo elétrico a ser removido do banco de dados.</param>
        Task DeleteAsync(int id);
    }
}
