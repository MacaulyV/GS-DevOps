using System.Collections.Generic;

namespace GestaoVeiculos.Models
{
    /// <summary>
    /// Representa um usuário no sistema de gestão de veículos.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int ID_Usuario { get; set; }

        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuário. Utilizado para login e comunicação.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário. Utilizada para autenticação no sistema.
        /// </summary>
        /// <remarks>
        /// Nota: Certifique-se de que a senha é armazenada de forma segura, como utilizando hashing.
        /// </remarks>
        public string Senha { get; set; }

        /// <summary>
        /// Lista de veículos de combustão pertencentes ao usuário.
        /// </summary>
        public List<VeiculoCombustao> VeiculosCombustao { get; set; }

        /// <summary>
        /// Lista de veículos elétricos pertencentes ao usuário.
        /// </summary>
        public List<VeiculoEletrico> VeiculosEletricos { get; set; }
    }
}
