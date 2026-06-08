using MovieManagement.Business.Services;
using MovieManagement.Data;
using MovieManagement.Data.Repositories;
using MovieManagement.Domain.Interfaces;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Enums;
using System;

namespace MovieManagementUi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Declaração das variáveis das interfaces (começam vazias)
            IFilmeRepository repositorio;
            ICategoriaRepository categoriaRepositorio;
            IRealizadorRepository realizadorRepositorio;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // =============================================================
            // SELEÇÃO DO MODO DE FUNCIONAMENTO (Menu Inicial Dinâmico)
            // =============================================================
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("       SISTEMA DE GESTÃO DE CINEMA - CONFIG      ");
                Console.WriteLine("=================================================");
                Console.WriteLine("1. Modo de Demonstração (Memória Temporária)");
                Console.WriteLine("2. Modo de Produção (Base de Dados SQLite)");
                Console.WriteLine("=================================================");
                Console.Write("Escolha o modo de funcionamento: ");

                string escolha = Console.ReadLine() ?? "";

                if (escolha == "1")
                {
                    repositorio = new FilmeMemoriaRepository();
                    categoriaRepositorio = new CategoriaMemoriaRepository();
                    realizadorRepositorio = new RealizadorMemoriaRepository();

                    ConfigurarDadosDeTesteEmMemoria(repositorio, categoriaRepositorio, realizadorRepositorio);
                    break;
                }
                else if (escolha == "2")
                {
                    repositorio = new FilmeSqlRepository();
                    categoriaRepositorio = new CategoriaSqlRepository();
                    realizadorRepositorio = new RealizadorSqlRepository();

                    break;
                }

                Console.WriteLine("\n❌ Opção inválida! Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
            }

            var servico = new MovieServices(repositorio, categoriaRepositorio, realizadorRepositorio);
            
            var categoriaServico = new CategoriaServices(categoriaRepositorio);
            var realizadorServico = new RealizadorServices(realizadorRepositorio);

            var menuPrincipal = new MovieMenu(servico, categoriaServico, realizadorServico);
            menuPrincipal.Exibir();
        }

        /// Método auxiliar para injetar dados iniciais na memória
        private static void ConfigurarDadosDeTesteEmMemoria(
            IFilmeRepository filmeRepo, 
            ICategoriaRepository catRepo, 
            IRealizadorRepository realRepo)
        {
            var catFiccao = new Categoria { Nome = "Ficção Científica" };
            var catTerror = new Categoria { Nome = "Terror" };
            var catAccao = new Categoria { Nome = "Acção" };

            catRepo.AdicionarCategoria(catFiccao);
            catRepo.AdicionarCategoria(catTerror);
            catRepo.AdicionarCategoria(catAccao);

            var refNolan = new Realizador { Nome = "Christopher Nolan", Pais = "Reino Unido" };
            var refTarantino = new Realizador { Nome = "Quentin Tarantino", Pais = "Estados Unidos" };
            var refSpielberg = new Realizador { Nome = "Steven Spielberg", Pais = "Estados Unidos" };

            realRepo.AdicionarRealizador(refNolan);
            realRepo.AdicionarRealizador(refTarantino);
            realRepo.AdicionarRealizador(refSpielberg);

            filmeRepo.AdicionarFilme(new Filme
            {
                Titulo = "Jurassic Park",
                Ano = 1993,
                Classificacao = Classificacao.Excelente, // Usando o Enum diretamente de forma limpa
                Lingua = "Inglês",
                CategoriaId = catFiccao.Id,     // Usa o ID gerado automaticamente
                RealizadorId = refSpielberg.Id, // Usa o ID gerado automaticamente
                Categoria = catFiccao,
                Realizador = refSpielberg
            });
        }
    }
}