namespace GestaoVeiculos.DTOs.ComparacaoEficiencia
{
    /// <summary>
    /// Data Transfer Object (DTO) contendo os resultados dos cálculos de eficiência para o veículo de combustão.
    /// </summary>
    public class ResultadoVeiculoCombustaoDTO
    {
        /// <summary>
        /// Modelo do veículo de combustão.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Quilometragem mensal percorrida pelo veículo de combustão.
        /// </summary>
        public double QuilometragemMensal { get; set; }

        /// <summary>
        /// Quantidade de litros de combustível consumidos mensalmente pelo veículo.
        /// </summary>
        public double ConsumoLitrosMensal { get; set; }

        /// <summary>
        /// Número de abastecimentos necessários por mês para o veículo de combustão.
        /// </summary>
        public double AbastecimentosMensais { get; set; }
    }
}
