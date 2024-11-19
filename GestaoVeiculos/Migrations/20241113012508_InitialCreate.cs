using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoVeiculosApi.Migrations
{
    /// <summary>
    /// Classe de migração inicial para a criação das tabelas do banco de dados.
    /// </summary>
    /// <remarks>
    /// Esta migração cria as tabelas "Usuarios", "VeiculosCombustao" e "VeiculosEletricos", estabelecendo as chaves primárias e as relações entre as tabelas.
    /// </remarks>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Método responsável pela criação das tabelas e seus relacionamentos.
        /// </summary>
        /// <param name="migrationBuilder">Objeto utilizado para definir as operações de migração.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criação da tabela Usuarios
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_Usuario);
                });

            // Criação da tabela VeiculosCombustao e relacionamento com Usuarios
            migrationBuilder.CreateTable(
                name: "VeiculosCombustao",
                columns: table => new
                {
                    ID_Veiculo_Combustao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_Usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ano = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Quilometragem_Mensal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Consumo_Medio = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Autonomia_Tanque = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Tipo_Combustivel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosCombustao", x => x.ID_Veiculo_Combustao);
                    table.ForeignKey(
                        name: "FK_VeiculosCombustao_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            // Criação da tabela VeiculosEletricos e relacionamento com Usuarios
            migrationBuilder.CreateTable(
                name: "VeiculosEletricos",
                columns: table => new
                {
                    ID_Veiculo_Eletrico = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_Usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ano = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Consumo_Medio = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Autonomia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosEletricos", x => x.ID_Veiculo_Eletrico);
                    table.ForeignKey(
                        name: "FK_VeiculosEletricos_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            // Índices para otimizar as consultas nas tabelas de veículos
            migrationBuilder.CreateIndex(
                name: "IX_VeiculosCombustao_ID_Usuario",
                table: "VeiculosCombustao",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosEletricos_ID_Usuario",
                table: "VeiculosEletricos",
                column: "ID_Usuario");
        }

        /// <summary>
        /// Método responsável por reverter as operações de migração realizadas pelo método Up.
        /// </summary>
        /// <param name="migrationBuilder">Objeto utilizado para definir as operações de rollback da migração.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Exclusão da tabela VeiculosCombustao
            migrationBuilder.DropTable(
                name: "VeiculosCombustao");

            // Exclusão da tabela VeiculosEletricos
            migrationBuilder.DropTable(
                name: "VeiculosEletricos");

            // Exclusão da tabela Usuarios
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
