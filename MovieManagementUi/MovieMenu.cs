using MovieManagement.Business.Services;
using System;

namespace MovieManagementUi
{
    public class MovieMenu
    {
        private readonly FilmeSubMenu _filmeSubMenu;
        private readonly CategoriaSubMenu _categoriaSubMenu;
        private readonly RealizadorSubMenu _realizadorSubMenu;

        public MovieMenu(MovieServices movieServices, CategoriaServices categoriaServices, RealizadorServices realizadorServices)
        {
            // Instanciamos os sub-menus passando apenas os serviços de que eles precisam
            _filmeSubMenu = new FilmeSubMenu(movieServices, categoriaServices, realizadorServices);
            _categoriaSubMenu = new CategoriaSubMenu(categoriaServices);
            _realizadorSubMenu = new RealizadorSubMenu(realizadorServices);
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("======= GESTÃO DE FILMES =======");
                Console.WriteLine("\nBem-vindo ao sistema!");
                Console.WriteLine("Inicializar listas... OK");
                Console.WriteLine("\n================================");
                Console.WriteLine();
                Console.WriteLine("\n------- MENU PRINCIPAL -------");
                Console.WriteLine("1. Gerir Filmes");
                Console.WriteLine("2. Gerir Categorias");
                Console.WriteLine("3. Gerir Realizadores");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1: _filmeSubMenu.Exibir(); break;
                    case 2: _categoriaSubMenu.Exibir(); break;
                    case 3: _realizadorSubMenu.Exibir(); break;
                    case 0: Console.WriteLine("Aplicação terminada!"); break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.\n");
                        Console.WriteLine("Pressione qualquer tecla para continuar... ");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != 0);
        }
    }
}