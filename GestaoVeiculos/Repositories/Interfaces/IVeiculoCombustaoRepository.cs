using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Models;

namespace GestaoVeiculos.Repositories.Interfaces
{
    /// <summary>
    /// Interface para o repositório de veículos de combustão.
    /// Define os métodos necessários para o gerenciamento dos dados de veículos de combustão, proporcionando uma camada de abstração para as operações de CRUD.
    /// </summary>
    public interface IVeiculoCombustaoRepository
    {
        /// <summary>
        /// Obtém todos os veículos de combustão cadastrados no sistema.
        /// </summary>
        /// <returns>Uma lista assíncrona de objetos <see cref="VeiculoCombustao"/>.</returns>
        Task<IEnumerable<VeiculoCombustao>> GetAllAsync();

        /// <summary>
        /// Obtém um veículo de combustão pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo de combustão que será pesquisado.</param>
        /// <returns>Um objeto <see cref="VeiculoCombustao"/> se encontrado, ou <c>null</c> se não existir.</returns>
        Task<VeiculoCombustao> GetByIdAsync(int id);

        /// <summary>
        /// Adiciona um novo veículo de combustão ao sistema.
        /// </summary>
        /// <param name="veiculo">Dados do veículo de combustão a ser adicionado ao banco de dados.</param>
        Task AddAsync(VeiculoCombustao veiculo);

        /// <summary>
        /// Atualiza os dados de um veículo de combustão existente.
        /// </summary>
        /// <param name="veiculo">Dados atualizados do veículo de combustão que serão persistidos no banco de dados.</param>
        Task UpdateAsync(VeiculoCombustao veiculo);

        /// <summary>
        /// Remove um veículo de combustão do sistema com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo de combustão a ser removido do banco de dados.</param>
        Task DeleteAsync(int id);
    }
}
