﻿// <auto-generated />
using GestaoVeiculos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace GestaoVeiculosApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241113012508_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestaoVeiculos.Models.Usuario", b =>
                {
                    b.Property<int>("ID_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Usuario"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID_Usuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("GestaoVeiculos.Models.VeiculoEletrico", b =>
                {
                    b.Property<int>("ID_Veiculo_Eletrico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Veiculo_Eletrico"));

                    b.Property<int>("Ano")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Autonomia")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("Consumo_Medio")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("ID_Usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID_Veiculo_Eletrico");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("VeiculosEletricos");
                });

            modelBuilder.Entity("VeiculoCombustao", b =>
                {
                    b.Property<int>("ID_Veiculo_Combustao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Veiculo_Combustao"));

                    b.Property<int>("Ano")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("Autonomia_Tanque")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<double>("Consumo_Medio")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("ID_Usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("Quilometragem_Mensal")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<string>("Tipo_Combustivel")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID_Veiculo_Combustao");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("VeiculosCombustao");
                });

            modelBuilder.Entity("GestaoVeiculos.Models.VeiculoEletrico", b =>
                {
                    b.HasOne("GestaoVeiculos.Models.Usuario", "Usuario")
                        .WithMany("VeiculosEletricos")
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("VeiculoCombustao", b =>
                {
                    b.HasOne("GestaoVeiculos.Models.Usuario", "Usuario")
                        .WithMany("VeiculosCombustao")
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestaoVeiculos.Models.Usuario", b =>
                {
                    b.Navigation("VeiculosCombustao");

                    b.Navigation("VeiculosEletricos");
                });
#pragma warning restore 612, 618
        }
    }
}
