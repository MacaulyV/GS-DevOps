using Microsoft.EntityFrameworkCore;
using GestaoVeiculos.Models;

namespace GestaoVeiculos.Data
{
    /// <summary>
    /// Contexto da aplicação para Entity Framework Core, representando a conexão com o banco de dados e mapeando as entidades.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor que recebe as opções de configuração do contexto.
        /// </summary>
        /// <param name="options">Opções do contexto para configurar a conexão.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet que representa os usuários no banco de dados.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// DbSet que representa os veículos de combustão no banco de dados.
        /// </summary>
        public DbSet<VeiculoCombustao> VeiculosCombustao { get; set; }

        /// <summary>
        /// DbSet que representa os veículos elétricos no banco de dados.
        /// </summary>
        public DbSet<VeiculoEletrico> VeiculosEletricos { get; set; }

        /// <summary>
        /// Configurações adicionais do modelo, como chaves primárias e relacionamentos entre entidades.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder utilizado para configurar as entidades do modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar a chave primária de Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.ID_Usuario); // Certifique-se de que 'ID_Usuario' é o nome correto da propriedade

            // Configurar a chave primária de VeiculoCombustao
            modelBuilder.Entity<VeiculoCombustao>()
                .HasKey(v => v.ID_Veiculo_Combustao); // Certifique-se de que 'ID_Veiculo_Combustao' é o nome correto da propriedade

            // Configurar a chave primária de VeiculoEletrico
            modelBuilder.Entity<VeiculoEletrico>()
                .HasKey(v => v.ID_Veiculo_Eletrico); // Certifique-se de que 'ID_Veiculo_Eletrico' é o nome correto da propriedade

            // Configurar o relacionamento entre Usuario e VeiculoCombustao (um usuário tem vários veículos de combustão)
            modelBuilder.Entity<VeiculoCombustao>()
                .HasOne(v => v.Usuario) // Um veículo de combustão pertence a um usuário
                .WithMany(u => u.VeiculosCombustao) // Um usuário pode ter vários veículos de combustão
                .HasForeignKey(v => v.ID_Usuario) // Chave estrangeira que vincula o veículo ao usuário
                .OnDelete(DeleteBehavior.Cascade); // Se um usuário for excluído, seus veículos de combustão também serão excluídos

            // Configurar o relacionamento entre Usuario e VeiculoEletrico (um usuário tem vários veículos elétricos)
            modelBuilder.Entity<VeiculoEletrico>()
                .HasOne(v => v.Usuario) // Um veículo elétrico pertence a um usuário
                .WithMany(u => u.VeiculosEletricos) // Um usuário pode ter vários veículos elétricos
                .HasForeignKey(v => v.ID_Usuario) // Chave estrangeira que vincula o veículo ao usuário
                .OnDelete(DeleteBehavior.Cascade); // Se um usuário for excluído, seus veículos elétricos também serão excluídos

            // Outras configurações, se necessário
        }
    }
}
