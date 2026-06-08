using MovieManagement.Business.Services;
using MovieManagement.Data;
using MovieManagement.Data.Repositories;
using MovieManagement.Domain.Interfaces;
using MovieManagement.Domain.Entities;
using System;
using MovieManagement.Domain.Enums;

namespace MovieManagementUi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =============================================================
            // Controlo de persistência de dados
            // =============================================================

            // OPÇÃO 1: Memória (List<T>)
            //
            IFilmeRepository repositorio = new FilmeMemoriaRepository();
            ICategoriaRepository categoriaRepositorio = new CategoriaMemoriaRepository();
            IRealizadorRepository realizadorRepositorio = new RealizadorMemoriaRepository();

            // -------------------------------------------------------------
            //
            // OPÇÃO 2: Base de dados (SQLite)
            //
            // IFilmeRepository repositorio = new FilmeSqlRepository();
            // ICategoriaRepository categoriaRepositorio = new CategoriaSqlRepository();
            // IRealizadorRepository realizadorRepositorio = new RealizadorSqlRepository();


            // =============================================================
            // INSTANCIAÇÃO INICIAL (para maior comodidade do utilizador)
            // =============================================================
            var catFiccao = new Categoria { Nome = "Ficção Científica" };
            var catTerror = new Categoria { Nome = "Terror" };
            var catAccao = new Categoria { Nome = "Acção" };

            try
            {
                categoriaRepositorio.AdicionarCategoria(catFiccao);
                categoriaRepositorio.AdicionarCategoria(catTerror);
                categoriaRepositorio.AdicionarCategoria(catAccao);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nota: Erro ou duplicação nas Categorias: {ex.Message}");
            }

            var refNolan = new Realizador { Nome = "Christopher Nolan", Pais = "Reino Unido" };
            var refTarantino = new Realizador { Nome = "Quentin Tarantino", Pais = "Estados Unidos" };
            var refSpielberg = new Realizador { Nome = "Steven Spielberg", Pais = "Estados Unidos" };

            try
            {
                realizadorRepositorio.AdicionarRealizador(refNolan);
                realizadorRepositorio.AdicionarRealizador(refTarantino);
                realizadorRepositorio.AdicionarRealizador(refSpielberg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nota: Erro ou duplicação nos Realizadores: {ex.Message}");
            }

            try
            {
                repositorio.AdicionarFilme(new Filme
                {
                    Titulo = "Jurassic Park",
                    Ano = 1993,
                    Classificacao = (Classificacao)5,
                    Lingua = "Inglês",
                    CategoriaId = 1,
                    RealizadorId = 3,
                    Categoria = catFiccao,
                    Realizador = refSpielberg
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nota: Erro ou duplicação no Filme Inicial: {ex.Message}");
            }
            // =============================================================

            // Garantir que BD e tabelas existem
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            var servico = new MovieServices(repositorio);
            var categoriaServico = new CategoriaServices(categoriaRepositorio);
            var realizadorServico = new RealizadorServices(realizadorRepositorio);

            var menu = new MovieMenu(servico, categoriaServico, realizadorServico);
            menu.Exibir();
        }
    }
}