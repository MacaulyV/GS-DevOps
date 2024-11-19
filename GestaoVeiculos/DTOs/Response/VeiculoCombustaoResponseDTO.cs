namespace GestaoVeiculos.DTOs.Response
{
    /// <summary>
    /// Data Transfer Object (DTO) para retornar informações detalhadas sobre um veículo de combustão.
    /// </summary>
    public class VeiculoCombustaoResponseDTO
    {
        /// <summary>
        /// Modelo do veículo de combustão.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo de combustão.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo de combustão.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Quilometragem mensal percorrida pelo veículo de combustão.
        /// </summary>
        public double Quilometragem_Mensal { get; set; }

        /// <summary>
        /// Consumo médio de combustível do veículo de combustão (em km/l).
        /// </summary>
        public double Consumo_Medio { get; set; }

        /// <summary>
        /// Capacidade total do tanque de combustível do veículo de combustão (em litros).
        /// </summary>
        public double Autonomia_Tanque { get; set; }

        /// <summary>
        /// Tipo de combustível utilizado pelo veículo de combustão.
        /// </summary>
        public string Tipo_Combustivel { get; set; }
        public int ID_Veiculo_Combustao { get; internal set; }
        public int ID_Usuario { get; internal set; }
    }
}
