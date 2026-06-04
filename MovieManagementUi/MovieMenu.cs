using MovieManagement.Business.Services;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Enums;
using System;

namespace MovieManagementUi
{
    public class MovieMenu
    {
        private readonly MovieServices _movieServices;
        public MovieMenu(MovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.WriteLine("=== GESTÃO DE FILMES (Parte 1) ===");
                Console.WriteLine("1. Adicionar filme");
                Console.WriteLine("2. Listar todos os filmes");
                Console.WriteLine("3. Pesquisar filme por título");
                Console.WriteLine("4. Remover filme");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        MenuAdicionarFilme();
                        break;
                    case 2:
                        MenuListarFilmes();
                        break;
                    case 3:
                        MenuPesquisarFilme();
                        break;
                    case 4:
                        MenuRemoverFilme();
                        break;
                    case 0:
                        Console.WriteLine("Aplicação terminada!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.\n");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
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
            if (int.TryParse(Console.ReadLine(), out int ano))
            {
                novoFilme.Ano = ano;
            }

            Console.Write("Língua: ");
            novoFilme.Lingua = Console.ReadLine() ?? "";

            Console.WriteLine("Classificação (0-Péssimo, 1-Fraco, 2-Mau, 3-Médio, 4-Bom, 5-Excelente): ");
            Console.Write("Escolha a nota (0 a 5): ");
            if (int.TryParse(Console.ReadLine(), out int nota) && nota >= 0 && nota <= 5)
            {
                novoFilme.Classificacao = (Classificacao)nota;
            }

            try
            {
                _movieServices.AdicionarFilme(novoFilme);
                Console.WriteLine("\n[SUCESSO] Filme adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO] {ex.Message}");
            }
            Console.WriteLine();
        }

        private void MenuListarFilmes()
        {
            Console.WriteLine("--- LISTA DE FILMES ---");
            var filmes = _movieServices.ListarFilmes();

            if (filmes.Count == 0)
            {
                Console.WriteLine("Nenhum filme registado de momento.");
                Console.WriteLine();
                return;
            }

            foreach (var f in filmes)
            {
                Console.WriteLine($"ID: {f.Id} | {f.Titulo} ({f.Ano}) - Língua: {f.Lingua} | Nota: {f.Classificacao} ({(int)f.Classificacao}/5)");
            }
            Console.WriteLine();
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
                    Console.WriteLine($"\n[Encontrado] ID: {filme.Id} | {filme.Titulo} ({filme.Ano}) - {filme.Classificacao}");
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
            Console.WriteLine();
        }

        private void MenuRemoverFilme()
        {
            Console.WriteLine("--- REMOVER FILME ---");
            Console.Write("Introduza o ID do filme a remover: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    _movieServices.RemoverFilme(id);
                    Console.WriteLine("\n[SUCESSO] O filme foi apagado!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO] {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\n[ERRO] Por favor, introduza um ID numérico válido.");
            }
            Console.WriteLine();
        }
    }
}