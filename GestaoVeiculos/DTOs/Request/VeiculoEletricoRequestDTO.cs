namespace GestaoVeiculos.DTOs.Request
{
    /// <summary>
    /// Data Transfer Object (DTO) para a criação ou atualização de um veículo elétrico.
    /// </summary>
    public class VeiculoEletricoRequestDTO
    {
        /// <summary>
        /// ID do usuário proprietário do veículo. Campo obrigatório.
        /// </summary>
        public int ID_Usuario { get; set; }

        /// <summary>
        /// Modelo do veículo elétrico. Campo obrigatório.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo elétrico. Campo obrigatório.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo elétrico. Campo obrigatório.
        /// </summary>
        public int Ano { get; set; }
    }
}
