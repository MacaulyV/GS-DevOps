using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoVeiculos.Data;
using GestaoVeiculos.Models;
using GestaoVeiculos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoVeiculos.Repositories
{
    /// <summary>
    /// Repositório de veículos elétricos que implementa a interface <see cref="IVeiculoEletricoRepository"/>.
    /// Responsável por executar operações de persistência de dados relacionadas aos veículos elétricos.
    /// </summary>
    public class VeiculoEletricoRepository : IVeiculoEletricoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do <see cref="VeiculoEletricoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados injetado para acessar os dados.</param>
        public VeiculoEletricoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os veículos elétricos cadastrados no banco de dados.
        /// </summary>
        /// <returns>Uma lista de todos os objetos <see cref="VeiculoEletrico"/> cadastrados.</returns>
        public async Task<IEnumerable<VeiculoEletrico>> GetAllAsync()
        {
            return await _context.VeiculosEletricos.ToListAsync();
        }

        /// <summary>
        /// Obtém um veículo elétrico específico pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo elétrico a ser pesquisado.</param>
        /// <returns>O objeto <see cref="VeiculoEletrico"/> correspondente ao ID, ou <c>null</c> se não for encontrado.</returns>
        public async Task<VeiculoEletrico> GetByIdAsync(int id)
        {
            return await _context.VeiculosEletricos.FindAsync(id);
        }

        /// <summary>
        /// Adiciona um novo veículo elétrico ao banco de dados.
        /// </summary>
        /// <param name="veiculo">Objeto <see cref="VeiculoEletrico"/> contendo os dados do novo veículo a ser adicionado.</param>
        /// <remarks>Depois de adicionar o veículo, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task AddAsync(VeiculoEletrico veiculo)
        {
            await _context.VeiculosEletricos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza os dados de um veículo elétrico existente no banco de dados.
        /// </summary>
        /// <param name="veiculo">Objeto <see cref="VeiculoEletrico"/> contendo os dados atualizados.</param>
        /// <remarks>Depois de atualizar o veículo, a operação <c>SaveChangesAsync()</c> é chamada para salvar as alterações no banco de dados.</remarks>
        public async Task UpdateAsync(VeiculoEletrico veiculo)
        {
            _context.VeiculosEletricos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove um veículo elétrico do banco de dados pelo ID fornecido.
        /// </summary>
        /// <param name="id">ID do veículo elétrico a ser removido.</param>
        /// <remarks>
        /// Primeiro, busca o veículo pelo ID. Se o veículo for encontrado, ele é removido do contexto
        /// e <c>SaveChangesAsync()</c> é chamado para salvar a alteração no banco de dados.
        /// </remarks>
        public async Task DeleteAsync(int id)
        {
            var veiculo = await _context.VeiculosEletricos.FindAsync(id);
            if (veiculo != null)
            {
                _context.VeiculosEletricos.Remove(veiculo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
