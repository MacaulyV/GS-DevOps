namespace GestaoVeiculos.DTOs.Response
{
    public class VeiculoEletricoResponseDTO
    {
        // ID do veículo elétrico - Identificador único para cada veículo.
        public int Id { get; set; }

        // ID específico do veículo elétrico (pode ser usado para diferenciar de outros tipos de veículos).
        public int ID_Veiculo_Eletrico { get; set; } // Adicionado

        // ID do usuário proprietário do veículo.
        public int ID_Usuario { get; set; }          // Adicionado

        // Modelo do veículo (ex.: Model S, i3, etc.).
        public string Modelo { get; set; }

        // Marca do veículo (ex.: Tesla, BMW, etc.).
        public string Marca { get; set; }

        // Ano de fabricação do veículo.
        public int Ano { get; set; }

        // Consumo médio do veículo em kWh por 100 km.
        public double Consumo_Medio { get; set; }

        // Autonomia do veículo em quilômetros (distância que pode percorrer com a carga cheia).
        public int Autonomia { get; set; }
    }
}
