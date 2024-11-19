using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Data;
using GestaoVeiculos.Models;
using GestaoVeiculos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoVeiculos.Repositories
{
    /// <summary>
    /// Repositório de veículos de combustão que implementa a interface <see cref="IVeiculoCombustaoRepository"/>.
    /// Responsável por executar operações de persistência de dados relacionadas aos veículos de combustão.
    /// </summary>
    public class VeiculoCombustaoRepository : IVeiculoCombustaoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do <see cref="VeiculoCombustaoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados injetado para acessar os dados.</param>
        public VeiculoCombustaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os veículos de combustão cadastrados no banco de dados.
        /// </summary>
        /// <returns>Uma lista de todos os objetos <see cref="VeiculoCombustao"/> cadastrados.</returns>
        public async Task<IEnumerable<VeiculoCombustao>> GetAllAsync()
        {
            return await _context.VeiculosCombustao.ToListAsync();
        }

        /// <summary>
        /// Obtém um veículo de combustão específico pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo de combustão a ser pesquisado.</param>
        /// <returns>O objeto <see cref="VeiculoCombustao"/> correspondente ao ID, ou <c>null</c> se não for encontrado.</returns>
        public async Task<VeiculoCombustao> GetByIdAsync(int id)
        {
            return await _context.VeiculosCombustao.FindAsync(id);
        }

        /// <summary>
        /// Adiciona um novo veículo de combustão ao banco de dados.
        /// </summary>
        /// <param name="veiculo">Objeto <see cref="VeiculoCombustao"/> contendo os dados do novo veículo a ser adicionado.</param>
        /// <remarks>Depois de adicionar o veículo, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task AddAsync(VeiculoCombustao veiculo)
        {
            _context.VeiculosCombustao.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza os dados de um veículo de combustão existente no banco de dados.
        /// </summary>
        /// <param name="veiculo">Objeto <see cref="VeiculoCombustao"/> contendo os dados atualizados.</param>
        /// <remarks>Depois de atualizar o veículo, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task UpdateAsync(VeiculoCombustao veiculo)
        {
            _context.VeiculosCombustao.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um veículo de combustão do banco de dados pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo de combustão a ser removido.</param>
        /// <remarks>
        /// Primeiro, busca o veículo pelo ID. Se o veículo for encontrado, ele é removido do contexto
        /// e <c>SaveChangesAsync()</c> é chamado para salvar a alteração no banco de dados.
        /// </remarks>
        public async Task DeleteAsync(int id)
        {
            var veiculo = await _context.VeiculosCombustao.FindAsync(id);
            if (veiculo != null)
            {
                _context.VeiculosCombustao.Remove(veiculo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
