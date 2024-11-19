namespace GestaoVeiculos.DTOs.Response
{
    /// <summary>
    /// Data Transfer Object (DTO) para retornar informações detalhadas sobre um veículo elétrico.
    /// </summary>
    public class VeiculoEletricoResponseDTO
    {
        /// <summary>
        /// Modelo do veículo elétrico.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo elétrico.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo elétrico.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Consumo médio de energia do veículo elétrico (em kWh por km).
        /// </summary>
        public double Consumo_Medio { get; set; }

        /// <summary>
        /// Autonomia do veículo elétrico, ou seja, a distância que ele pode percorrer com uma carga completa (em quilômetros).
        /// </summary>
        public int Autonomia { get; set; }
        public int ID_Veiculo_Eletrico { get; internal set; }
        public int ID_Usuario { get; internal set; }
    }
}
