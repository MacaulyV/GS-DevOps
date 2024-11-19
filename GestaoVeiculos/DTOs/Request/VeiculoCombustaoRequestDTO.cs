namespace GestaoVeiculos.DTOs.Request
{
    /// <summary>
    /// Data Transfer Object (DTO) para a criação ou atualização de um veículo de combustão.
    /// </summary>
    public class VeiculoCombustaoRequestDTO
    {
        /// <summary>
        /// ID do usuário proprietário do veículo. Campo obrigatório.
        /// </summary>
        public int ID_Usuario { get; set; }

        /// <summary>
        /// Modelo do veículo de combustão. Campo obrigatório.
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Marca do veículo de combustão. Campo obrigatório.
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Ano de fabricação do veículo de combustão. Campo obrigatório.
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// Quilometragem mensal percorrida pelo veículo de combustão. Campo obrigatório.
        /// </summary>
        public double Quilometragem_Mensal { get; set; }
    }
}
