namespace GestaoVeiculos.DTOs.Request
{
    /// <summary>
    /// Data Transfer Object (DTO) para a criação ou atualização de um usuário.
    /// </summary>
    public class UsuarioRequestDTO
    {
        /// <summary>
        /// Nome do usuário. Campo obrigatório ao criar ou atualizar um usuário.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuário. Campo obrigatório ao criar ou atualizar um usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário. Campo obrigatório ao criar ou atualizar um usuário.
        /// </summary>
        public string Senha { get; set; }
    }
}
