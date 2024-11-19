using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoVeiculos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Tipo_Combustivel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quilometragem_Media_Mensal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Consumo_Medio = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Autonomia_Tanque = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Quilometragem_Mensal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    UsuarioID_Usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosCombustao", x => x.ID_Veiculo_Combustao);
                    table.ForeignKey(
                        name: "FK_VeiculosCombustao_Usuarios_UsuarioID_Usuario",
                        column: x => x.UsuarioID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Autonomia = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Quilometragem_Mensal = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    UsuarioID_Usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosEletricos", x => x.ID_Veiculo_Eletrico);
                    table.ForeignKey(
                        name: "FK_VeiculosEletricos_Usuarios_UsuarioID_Usuario",
                        column: x => x.UsuarioID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosCombustao_UsuarioID_Usuario",
                table: "VeiculosCombustao",
                column: "UsuarioID_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosEletricos_UsuarioID_Usuario",
                table: "VeiculosEletricos",
                column: "UsuarioID_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeiculosCombustao");

            migrationBuilder.DropTable(
                name: "VeiculosEletricos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
