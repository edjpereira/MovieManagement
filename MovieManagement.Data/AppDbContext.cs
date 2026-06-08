using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Entities;

namespace MovieManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Realizador> Realizadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cinema.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categoria padrão se a BD for nova
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Ficção Científica" },
                new Categoria { Id = 2, Nome = "Terror" },
                new Categoria { Id = 3, Nome = "Ação" }
            );

            // Realizador padrão
            modelBuilder.Entity<Realizador>().HasData(
                new Realizador { Id = 1, Nome = "Christopher Nolan", Pais = "Reino Unido" },
                new Realizador { Id = 2, Nome = "Quentin Tarantino", Pais = "Estados Unidos" },
                new Realizador { Id = 3, Nome = "Steven Spielberg", Pais = "Estados Unidos da América" }
            );
        }
    }
}