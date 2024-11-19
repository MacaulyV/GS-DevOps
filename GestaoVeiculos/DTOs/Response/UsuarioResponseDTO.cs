namespace GestaoVeiculos.DTOs.Response
{
    /// <summary>
    /// Data Transfer Object (DTO) para retornar informações de um usuário.
    /// </summary>
    public class UsuarioResponseDTO
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuário.
        /// </summary>
        public string Email { get; set; }
    }
}
