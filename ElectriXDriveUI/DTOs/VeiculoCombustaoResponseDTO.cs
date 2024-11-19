namespace ElectriXDriveUI.DTOs
{
    public class VeiculoCombustaoResponseDTO
    {
        // ID do veículo - Identificador único para cada veículo de combustão.
        public int Id { get; set; }

        // Marca do veículo (ex.: Ford, Toyota, etc.).
        public string Marca { get; set; }

        // Modelo do veículo (ex.: Fiesta, Corolla, etc.).
        public string Modelo { get; set; }

        // Consumo médio do veículo em quilômetros por litro.
        public double Consumo_Medio { get; set; }

        // Autonomia do tanque em quilômetros.
        public int Autonomia_Tanque { get; set; }

        // Tipo de combustível utilizado (ex.: Gasolina, Diesel, etc.).
        public string Tipo_Combustivel { get; set; }
    }
}
