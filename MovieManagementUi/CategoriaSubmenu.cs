using MovieManagement.Business.Services;
using MovieManagement.Domain.Entities;
using System;

namespace MovieManagementUi
{
    public class CategoriaSubMenu
    {
        private readonly CategoriaServices _categoriaServices;

        public CategoriaSubMenu(CategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SUB-MENU CATEGORIAS ===");
                Console.WriteLine("1. Adicionar categoria");
                Console.WriteLine("2. Listar categorias");
                Console.WriteLine("3. Remover categoria");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                Console.WriteLine();

                switch (opcao)
                {
                    case 1: MenuAdicionarCategoria(); break;
                    case 2: MenuListarCategorias(); break;
                    case 3: MenuRemoverCategoria(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu de Categorias...");
                    Console.ReadKey();
                }
            } while (opcao != 0);
        }

        private void MenuAdicionarCategoria()
        {
            Console.WriteLine("--- ADICIONAR NOVA CATEGORIA ---");
            var novaCategoria = new Categoria();
            Console.Write("Nome da Categoria: ");
            novaCategoria.Nome = Console.ReadLine() ?? "";

            try
            {
                _categoriaServices.AdicionarCategoria(novaCategoria);
                Console.WriteLine("\n[SUCESSO] Categoria adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO] {ex.Message}");
            }
        }

        private void MenuListarCategorias()
        {
            Console.WriteLine("--- LISTA DE CATEGORIAS ---");
            var categories = _categoriaServices.ListarCategorias();

            if (categories.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria registada.");
                return;
            }

            foreach (var cat in categories)
            {
                Console.WriteLine($"ID: {cat.Id} | {cat.Nome}");
            }
        }

        private void MenuRemoverCategoria()
        {
            Console.WriteLine("--- REMOVER CATEGORIA ---");
            Console.Write("Introduza o ID da categoria a remover: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_categoriaServices.RemoverCategoria(id))
                    Console.WriteLine("\n✅ Sucesso: A categoria foi removida!");
                else
                    Console.WriteLine("\n❌ Erro: ID não encontrado.");
            }
        }
    }
}