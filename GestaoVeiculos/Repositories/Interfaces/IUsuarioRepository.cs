using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Models;

namespace GestaoVeiculos.Repositories.Interfaces
{
    /// <summary>
    /// Interface para o repositório de usuários.
    /// Define os métodos necessários para gerenciamento de dados de usuários, oferecendo uma abstração para operações comuns.
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtém todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Uma lista assíncrona de objetos <see cref="Usuario"/>.</returns>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtém um usuário pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do usuário que será pesquisado.</param>
        /// <returns>Um objeto <see cref="Usuario"/> se encontrado, ou <c>null</c> se não existir.</returns>
        Task<Usuario> GetByIdAsync(int id);

        /// <summary>
        /// Obtém um usuário pelo endereço de email fornecido.
        /// </summary>
        /// <param name="email">Email do usuário a ser pesquisado.</param>
        /// <returns>Um objeto <see cref="Usuario"/> se encontrado, ou <c>null</c> se não existir.</returns>
        Task<Usuario> GetByEmailAsync(string email);

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser adicionado ao banco de dados.</param>
        Task AddAsync(Usuario usuario);

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuario">Dados atualizados do usuário que serão persistidos no banco de dados.</param>
        Task UpdateAsync(Usuario usuario);

        /// <summary>
        /// Remove um usuário do sistema com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido do banco de dados.</param>
        Task DeleteAsync(int id);
    }
}
