using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Data;
using GestaoVeiculos.Models;
using GestaoVeiculos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoVeiculos.Repositories
{
    /// <summary>
    /// Repositório de usuários que implementa a interface <see cref="IUsuarioRepository"/>.
    /// Responsável por executar operações de persistência de dados relacionadas aos usuários.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do <see cref="UsuarioRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados injetado para acessar os dados.</param>
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os usuários cadastrados no banco de dados.
        /// </summary>
        /// <returns>Uma lista de todos os objetos <see cref="Usuario"/> cadastrados.</returns>
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Obtém um usuário específico pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do usuário a ser pesquisado.</param>
        /// <returns>O objeto <see cref="Usuario"/> correspondente ao ID, ou <c>null</c> se não for encontrado.</returns>
        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        /// <summary>
        /// Obtém um usuário específico pelo endereço de email.
        /// </summary>
        /// <param name="email">Email do usuário a ser pesquisado.</param>
        /// <returns>O objeto <see cref="Usuario"/> correspondente ao email, ou <c>null</c> se não for encontrado.</returns>
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Adiciona um novo usuário ao banco de dados.
        /// </summary>
        /// <param name="usuario">Objeto <see cref="Usuario"/> contendo os dados do novo usuário a ser adicionado.</param>
        /// <remarks>Depois de adicionar o usuário, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente no banco de dados.
        /// </summary>
        /// <param name="usuario">Objeto <see cref="Usuario"/> contendo os dados atualizados.</param>
        /// <remarks>Depois de atualizar o usuário, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um usuário do banco de dados pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        /// <remarks>
        /// Primeiro, busca o usuário pelo ID. Se o usuário for encontrado, ele é removido do contexto
        /// e <c>SaveChangesAsync()</c> é chamado para salvar a alteração no banco de dados.
        /// </remarks>
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
