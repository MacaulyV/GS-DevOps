using GestaoVeiculos.DTOs.Response;

public interface IVeiculoEletricoRepository
{
    // Método para obter todos os veículos elétricos.
    Task<IEnumerable<VeiculoEletricoResponseDTO>> GetAllAsync();

    // Método para obter um veículo elétrico específico pelo ID.
    Task<VeiculoEletricoResponseDTO> GetByIdAsync(int id);

    // Método para adicionar um novo veículo elétrico.
    Task AddAsync(VeiculoEletricoResponseDTO veiculoEletrico);

    // Método para atualizar um veículo elétrico existente.
    Task UpdateAsync(VeiculoEletricoResponseDTO veiculoEletrico);

    // Método para deletar um veículo elétrico pelo ID.
    Task DeleteAsync(int id);
}
