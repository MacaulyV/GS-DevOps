using GestaoVeiculos.DTOs.Response;

public class VeiculoEletricoRepository : IVeiculoEletricoRepository
{
    private readonly HttpClient _httpClient;

    // Construtor que injeta o HttpClient para fazer as requisições HTTP à API.
    public VeiculoEletricoRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obter todos os veículos elétricos da API.
    public async Task<IEnumerable<VeiculoEletricoResponseDTO>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("https://sua-api.com/api/veiculoseletricos");
        response.EnsureSuccessStatusCode(); // Verifica se a requisição foi bem-sucedida.

        // Lê o conteúdo da resposta e converte para uma lista de objetos VeiculoEletricoResponseDTO.
        return await response.Content.ReadFromJsonAsync<IEnumerable<VeiculoEletricoResponseDTO>>();
    }

    // Método para obter um veículo elétrico específico pelo ID.
    public async Task<VeiculoEletricoResponseDTO> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://sua-api.com/api/veiculoseletricos/{id}");
        response.EnsureSuccessStatusCode(); // Verifica se a requisição foi bem-sucedida.

        // Lê o conteúdo da resposta e converte para um objeto VeiculoEletricoResponseDTO.
        return await response.Content.ReadFromJsonAsync<VeiculoEletricoResponseDTO>();
    }

    // Método para adicionar um novo veículo elétrico à API.
    public async Task AddAsync(VeiculoEletricoResponseDTO veiculoEletrico)
    {
        var response = await _httpClient.PostAsJsonAsync("https://sua-api.com/api/veiculoseletricos", veiculoEletrico);
        response.EnsureSuccessStatusCode(); // Verifica se a adição foi bem-sucedida.
    }

    // Método para atualizar um veículo elétrico existente na API.
    public async Task UpdateAsync(VeiculoEletricoResponseDTO veiculoEletrico)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://sua-api.com/api/veiculoseletricos/{veiculoEletrico.Id}", veiculoEletrico);
        response.EnsureSuccessStatusCode(); // Verifica se a atualização foi bem-sucedida.
    }

    // Método para deletar um veículo elétrico pelo ID.
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://sua-api.com/api/veiculoseletricos/{id}");
        response.EnsureSuccessStatusCode(); // Verifica se a exclusão foi bem-sucedida.
    }
}
