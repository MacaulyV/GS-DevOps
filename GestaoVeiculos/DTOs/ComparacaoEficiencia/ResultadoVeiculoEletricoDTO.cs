namespace GestaoVeiculos.DTOs.ComparacaoEficiencia
{
    /// <summary>
    /// Data Transfer Object (DTO) contendo os resultados dos cálculos de eficiência para o veículo elétrico.
    /// </summary>
    public class ResultadoVeiculoEletricoDTO
    {
        /// <summary>
        /// Modelo do veículo elétrico.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Quilometragem mensal percorrida pelo veículo elétrico.
        /// </summary>
        public double QuilometragemMensal { get; set; }

        /// <summary>
        /// Número de cargas completas necessárias por mês para o veículo elétrico.
        /// </summary>
        public double CargasMensais { get; set; }
    }
}
