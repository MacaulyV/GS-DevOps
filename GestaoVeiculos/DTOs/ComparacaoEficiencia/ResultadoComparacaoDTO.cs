namespace GestaoVeiculos.DTOs.ComparacaoEficiencia
{
    /// <summary>
    /// Data Transfer Object (DTO) para retornar o resultado da comparação entre veículos de combustão e elétricos.
    /// </summary>
    public class ResultadoComparacaoDTO
    {
        /// <summary>
        /// Resultado dos cálculos de eficiência para o veículo de combustão.
        /// </summary>
        public ResultadoVeiculoCombustaoDTO VeiculoCombustao { get; set; }

        /// <summary>
        /// Resultado dos cálculos de eficiência para o veículo elétrico.
        /// </summary>
        public ResultadoVeiculoEletricoDTO VeiculoEletrico { get; set; }

        /// <summary>
        /// Explicação detalhada dos cálculos de comparação entre os veículos.
        /// </summary>
        public ExplicacaoDTO Explicacao { get; set; }
    }
}
