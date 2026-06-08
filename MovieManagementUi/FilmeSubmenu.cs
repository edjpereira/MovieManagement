using MovieManagement.Business.Services;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Enums;
using System;
using System.Linq;

namespace MovieManagementUi
{
    public class FilmeSubMenu
    {
        private readonly MovieServices _movieServices;
        private readonly CategoriaServices _categoriaServices;
        private readonly RealizadorServices _realizadorServices;

        public FilmeSubMenu(MovieServices movieServices, CategoriaServices categoriaServices, RealizadorServices realizadorServices)
        {
            _movieServices = movieServices;
            _categoriaServices = categoriaServices;
            _realizadorServices = realizadorServices;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SUB-MENU FILMES ===");
                Console.WriteLine("1. Adicionar filme");
                Console.WriteLine("2. Listar todos os filmes");
                Console.WriteLine("3. Pesquisar filme por título");
                Console.WriteLine("4. Remover filme");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                Console.WriteLine();

                switch (opcao)
                {
                    case 1: MenuAdicionarFilme(); break;
                    case 2: MenuListarFilmes(); break;
                    case 3: MenuPesquisarFilme(); break;
                    case 4: MenuRemoverFilme(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu de Filmes...");
                    Console.ReadKey();
                }
            } while (opcao != 0);
        }

        private void MenuAdicionarFilme()
        {
            Console.WriteLine("--- ADICIONAR NOVO FILME ---");
            var novoFilme = new Filme();

            Console.Write("Título: ");
            novoFilme.Titulo = Console.ReadLine() ?? "";

            Console.Write("Ano de Lançamento: ");
            if (int.TryParse(Console.ReadLine(), out int ano)) novoFilme.Ano = ano;

            Console.Write("Língua: ");
            novoFilme.Lingua = Console.ReadLine() ?? "";

            int classificacao;
            while (true)
            {
                Console.Write("Classificação (0 a 5): ");
                if (int.TryParse(Console.ReadLine(), out classificacao) && classificacao >= 0 && classificacao <= 5) break;
                Console.WriteLine("\n❌ Erro: Entrada inválida. A classificação deve ser entre 0 e 5.");
            }
            novoFilme.Classificacao = (Classificacao)classificacao;

            var categorias = _categoriaServices.ListarCategorias();
            Console.WriteLine("\n--- Categorias Disponíveis ---");
            foreach (var cat in categorias) Console.WriteLine($"[{cat.Id}] {cat.Nome}");

            int categoriaId;
            while (true)
            {
                Console.Write("Escolha o ID da Categoria: ");
                if (int.TryParse(Console.ReadLine(), out categoriaId) && categorias.Any(c => c.Id == categoriaId)) break;
                Console.WriteLine("❌ Categoria inválida. Escolha um ID da lista.");
            }

            var realizadores = _realizadorServices.ListarRealizadores();
            Console.WriteLine("\n--- Realizadores Disponíveis ---");
            foreach (var real in realizadores) Console.WriteLine($"[{real.Id}] {real.Nome} ({real.Pais})");

            int realizadorId;
            while (true)
            {
                Console.Write("Escolha o ID do Realizador: ");
                if (int.TryParse(Console.ReadLine(), out realizadorId) && realizadores.Any(r => r.Id == realizadorId)) break;
                Console.WriteLine("❌ Realizador inválido. Escolha um ID da lista.");
            }

            novoFilme.CategoriaId = categoriaId;
            novoFilme.RealizadorId = realizadorId;
            novoFilme.Categoria = categorias.First(c => c.Id == categoriaId);
            novoFilme.Realizador = realizadores.First(r => r.Id == realizadorId);

            try
            {
                _movieServices.AdicionarFilme(novoFilme);
                Console.WriteLine("\n[SUCESSO] Filme adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO] {ex.Message}");
            }
        }

        private void MenuListarFilmes()
        {
            Console.WriteLine("--- LISTA DE FILMES ---");
            var filmes = _movieServices.ListarFilmes();

            if (filmes.Count == 0)
            {
                Console.WriteLine("Nenhum filme registado de momento.\n");
                return;
            }

            foreach (var f in filmes)
            {
                Console.WriteLine($"ID: {f.Id} | {f.Titulo} ({f.Ano}) - Língua: {f.Lingua} | Nota: {f.Classificacao} ({(int)f.Classificacao}/5)"
                                    + $"\nRealizador: {f.Realizador?.Nome ?? "N/A"} | {f.Categoria?.Nome ?? "N/A"}\n");
            }
        }

        private void MenuPesquisarFilme()
        {
            Console.WriteLine("--- PESQUISAR FILME ---");
            Console.Write("Introduza o título a pesquisar: ");
            string termo = Console.ReadLine() ?? "";

            try
            {
                var filme = _movieServices.ObterFilmePorTitulo(termo);
                if (filme != null)
                {
                    Console.WriteLine($"\n[Encontrado] ID: {filme.Id} | {filme.Titulo} ({filme.Ano}) - {filme.Classificacao}"
                    + $"\nRealizador: {filme.Realizador?.Nome ?? "N/A"} | {filme.Categoria?.Nome ?? "N/A"}");
                }
                else
                {
                    Console.WriteLine("\n[Aviso] Nenhum filme encontrado com esse título.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO] {ex.Message}");
            }
        }

        private void MenuRemoverFilme()
        {
            Console.WriteLine("--- REMOVER FILME ---");
            Console.Write("Introduza o ID do filme a remover: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_movieServices.RemoverFilme(id))
                    Console.WriteLine("\n✅ Sucesso: O filme foi removido!");
                else
                    Console.WriteLine("\n❌ Erro: Não foi encontrado nenhum filme com esse ID.");
            }
            else
            {
                Console.WriteLine("\n⚠️ Por favor, digite um número de ID válido.");
            }
        }
    }
}