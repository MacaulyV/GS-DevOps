using System.ComponentModel.DataAnnotations;

namespace GestaoVeiculos.DTOs.Request
{
    /// <summary>
    /// Data Transfer Object (DTO) para solicitar o login de um usuário.
    /// </summary>
    public class LoginRequestDTO
    {
        /// <summary>
        /// Email do usuário. Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário. Campo obrigatório.
        /// </summary>
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
