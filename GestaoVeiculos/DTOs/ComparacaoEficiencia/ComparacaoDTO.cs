namespace GestaoVeiculos.DTOs.ComparacaoEficiencia
{
    /// <summary>
    /// Data Transfer Object (DTO) para receber os IDs dos veículos a serem comparados em termos de eficiência.
    /// </summary>
    public class ComparacaoDTO
    {
        /// <summary>
        /// ID do veículo de combustão a ser comparado.
        /// </summary>
        public int IdVeiculoCombustao { get; set; }

        /// <summary>
        /// ID do veículo elétrico a ser comparado.
        /// </summary>
        public int IdVeiculoEletrico { get; set; }
    }
}
