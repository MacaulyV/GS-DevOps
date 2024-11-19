using ElectriXDriveUI.DTOs;

public interface IVeiculoCombustaoRepository
{
    // Método para obter todos os veículos de combustão.
    Task<IEnumerable<VeiculoCombustaoResponseDTO>> GetAllAsync();

    // Método para obter um veículo de combustão específico pelo ID.
    Task<VeiculoCombustaoResponseDTO> GetByIdAsync(int id);

    // Método para adicionar um novo veículo de combustão.
    Task AddAsync(VeiculoCombustaoResponseDTO veiculoCombustao);

    // Método para atualizar um veículo de combustão existente.
    Task UpdateAsync(VeiculoCombustaoResponseDTO veiculoCombustao);

    // Método para deletar um veículo de combustão pelo ID.
    Task DeleteAsync(int id);
}
