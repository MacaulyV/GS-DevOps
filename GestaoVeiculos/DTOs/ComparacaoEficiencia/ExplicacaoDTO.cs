namespace GestaoVeiculos.DTOs.ComparacaoEficiencia
{
    /// <summary>
    /// Data Transfer Object (DTO) contendo as explicações detalhadas dos cálculos de eficiência para veículos de combustão e elétricos.
    /// </summary>
    public class ExplicacaoDTO
    {
        /// <summary>
        /// Explicação dos cálculos de eficiência para o veículo de combustão.
        /// </summary>
        public string VeiculoCombustao { get; set; }

        /// <summary>
        /// Explicação dos cálculos de eficiência para o veículo elétrico.
        /// </summary>
        public string VeiculoEletrico { get; set; }
    }
}
