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
                Console.WriteLine("3. Editar Categoria");
                Console.WriteLine("4. Remover categoria");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        MenuAdicionarCategoria();
                        break;
                    case 2:
                        MenuListarCategorias();
                        break;
                    case 3:
                        MenuEditarCategoria();
                        break;
                    case 4:
                        MenuRemoverCategoria();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
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

        private void MenuEditarCategoria()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("              EDITAR CATEGORIA                   ");
            Console.WriteLine("=================================================");

            var listaCategorias = _categoriaServices.ListarCategorias();

            if (listaCategorias.Count == 0)
            {
                Console.WriteLine("\n⚠️ Não existem categorias registadas.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCategorias no Sistema:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var c in listaCategorias)
            {
                Console.WriteLine($"[ID: {c.Id}] - {c.Nome}");
            }
            Console.WriteLine("-------------------------------------------------\n");

            Console.Write("Indique o ID da categoria que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                Console.ReadKey();
                return;
            }

            var categoria = listaCategorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                Console.WriteLine("❌ Categoria não encontrada!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n✍️ A editar: {categoria.Nome}");
            Console.WriteLine("(Pressione ENTER sem escrever nada para MANTER o valor atual)\n");

            Console.Write($"Novo Nome [{categoria.Nome}]: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                categoria.Nome = novoNome;
            }

            try
            {
                _categoriaServices.AtualizarCategoria(categoria);
                Console.WriteLine("\n✅ Categoria atualizada com sucesso!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\n❌ Erro ao atualizar: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private void MenuRemoverCategoria()
        {
            Console.WriteLine("--- REMOVER CATEGORIA ---");
            var listaCategorias = _categoriaServices.ListarCategorias();

            if (listaCategorias.Count == 0)
            {
                Console.WriteLine("\n⚠️ Não existem categorias registadas.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCategorias no Sistema:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var c in listaCategorias)
            {
                Console.WriteLine($"[ID: {c.Id}] - {c.Nome}");
            }
            Console.WriteLine("-------------------------------------------------\n");
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