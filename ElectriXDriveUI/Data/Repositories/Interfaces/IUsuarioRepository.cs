public interface IUsuarioRepository
{
    // Método para obter todos os usuários.
    Task<IEnumerable<UsuarioResponseDTO>> GetAllAsync();

    // Método para obter um usuário específico pelo ID.
    Task<UsuarioResponseDTO> GetByIdAsync(int id);

    // Método para adicionar um novo usuário.
    Task AddAsync(UsuarioResponseDTO usuario);

    // Método para atualizar um usuário existente.
    Task UpdateAsync(UsuarioResponseDTO usuario);

    // Método para deletar um usuário pelo ID.
    Task DeleteAsync(int id);
}
