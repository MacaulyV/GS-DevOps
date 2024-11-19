using ElectriXDriveUI.DTOs;

public class VeiculoCombustaoRepository : IVeiculoCombustaoRepository
{
    private readonly HttpClient _httpClient;

    // Construtor que injeta o HttpClient para fazer requisições HTTP à API.
    public VeiculoCombustaoRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obter todos os veículos de combustão da API.
    public async Task<IEnumerable<VeiculoCombustaoResponseDTO>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("https://sua-api.com/api/veiculosCombustao");
        response.EnsureSuccessStatusCode(); // Garante que a requisição foi bem-sucedida.

        // Lê o conteúdo da resposta e converte para uma lista de objetos VeiculoCombustaoResponseDTO.
        return await response.Content.ReadFromJsonAsync<IEnumerable<VeiculoCombustaoResponseDTO>>();
    }

    // Método para buscar um veículo de combustão específico pelo ID.
    public async Task<VeiculoCombustaoResponseDTO> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://sua-api.com/api/veiculosCombustao/{id}");
        response.EnsureSuccessStatusCode(); // Garante que a requisição foi bem-sucedida.

        // Lê o conteúdo da resposta e converte para um objeto VeiculoCombustaoResponseDTO.
        return await response.Content.ReadFromJsonAsync<VeiculoCombustaoResponseDTO>();
    }

    // Método para adicionar um novo veículo de combustão à API.
    public async Task AddAsync(VeiculoCombustaoResponseDTO veiculoCombustao)
    {
        var response = await _httpClient.PostAsJsonAsync("https://sua-api.com/api/veiculosCombustao", veiculoCombustao);
        response.EnsureSuccessStatusCode(); // Garante que a adição foi bem-sucedida.
    }

    // Método para atualizar um veículo de combustão existente.
    public async Task UpdateAsync(VeiculoCombustaoResponseDTO veiculoCombustao)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://sua-api.com/api/veiculosCombustao/{veiculoCombustao.Id}", veiculoCombustao);
        response.EnsureSuccessStatusCode(); // Garante que a atualização foi bem-sucedida.
    }

    // Método para deletar um veículo de combustão pelo ID.
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://sua-api.com/api/veiculosCombustao/{id}");
        response.EnsureSuccessStatusCode(); // Garante que a exclusão foi bem-sucedida.
    }
}
