public class UsuarioRepository : IUsuarioRepository
{
    private readonly HttpClient _httpClient;

    // Construtor que recebe um HttpClient para ser usado nas requisições à API.
    public UsuarioRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para buscar todos os usuários da API.
    public async Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("https://sua-api.com/api/usuarios");
        response.EnsureSuccessStatusCode(); // Garante que a requisição foi bem-sucedida, caso contrário lança uma exceção.

        // Lê o conteúdo da resposta e converte para uma lista de objetos UsuarioResponseDTO.
        return await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioResponseDTO>>();
    }

    // Método para buscar um usuário específico pelo ID.
    public async Task<UsuarioResponseDTO> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://sua-api.com/api/usuarios/{id}");
        response.EnsureSuccessStatusCode(); // Verifica se a requisição teve sucesso.

        // Lê o conteúdo da resposta e converte para um objeto UsuarioResponseDTO.
        return await response.Content.ReadFromJsonAsync<UsuarioResponseDTO>();
    }

    // Método para adicionar um novo usuário na API.
    public async Task AddAsync(UsuarioResponseDTO usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("https://sua-api.com/api/usuarios", usuario);
        response.EnsureSuccessStatusCode(); // Garante que a adição foi bem-sucedida.
    }

    // Método para atualizar um usuário existente.
    public async Task UpdateAsync(UsuarioResponseDTO usuario)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://sua-api.com/api/usuarios/{usuario.Id}", usuario);
        response.EnsureSuccessStatusCode(); // Garante que a atualização foi bem-sucedida.
    }

    // Método para deletar um usuário pelo ID.
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://sua-api.com/api/usuarios/{id}");
        response.EnsureSuccessStatusCode(); // Garante que a exclusão foi bem-sucedida.
    }
}
