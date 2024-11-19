namespace ElectriXDriveUI.ViewModels
{
    public class VeiculoEletricoViewModel
    {
        // ID do veículo elétrico - identificador único do veículo.
        public int ID_Veiculo_Eletrico { get; set; }

        // ID do usuário - identificador do proprietário do veículo.
        public int ID_Usuario { get; set; }

        // Modelo do veículo (ex.: Model S, Leaf, etc.).
        public string Modelo { get; set; }

        // Marca do veículo (ex.: Tesla, Nissan, etc.).
        public string Marca { get; set; }

        // Ano de fabricação do veículo.
        public int Ano { get; set; }

        // Consumo médio do veículo em kWh por 100 km.
        public double Consumo_Medio { get; set; }

        // Autonomia do veículo em quilômetros (distância que pode percorrer com a carga completa).
        public int Autonomia { get; set; }
    }
}
