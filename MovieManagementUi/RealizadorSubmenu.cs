using MovieManagement.Business.Services;
using MovieManagement.Domain.Entities;
using System;

namespace MovieManagementUi
{
    public class RealizadorSubMenu
    {
        private readonly RealizadorServices _realizadorServices;

        public RealizadorSubMenu(RealizadorServices realizadorServices)
        {
            _realizadorServices = realizadorServices;
        }

        public void Exibir()
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SUB-MENU REALIZADORES ===");
                Console.WriteLine("1. Adicionar realizador");
                Console.WriteLine("2. Listar realizadores");
                Console.WriteLine("3. Remover realizador");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                Console.WriteLine();

                switch (opcao)
                {
                    case 1: MenuAdicionarRealizador(); break;
                    case 2: MenuListarRealizadores(); break;
                    case 3: MenuRemoverRealizador(); break;
                    case 0: return;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu de Realizadores...");
                    Console.ReadKey();
                }
            } while (opcao != 0);
        }

        private void MenuAdicionarRealizador()
        {
            Console.WriteLine("--- ADICIONAR NOVO REALIZADOR ---");
            var novoRealizador = new Realizador();

            Console.Write("Nome do Realizador: ");
            novoRealizador.Nome = Console.ReadLine() ?? "";

            Console.Write("País de Origem: ");
            novoRealizador.Pais = Console.ReadLine() ?? "";

            try
            {
                _realizadorServices.AdicionarRealizador(novoRealizador);
                Console.WriteLine("\n[SUCESSO] Realizador adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO] {ex.Message}");
            }
        }

        private void MenuListarRealizadores()
        {
            Console.WriteLine("--- LISTA DE REALIZADORES ---");
            var realizadores = _realizadorServices.ListarRealizadores();

            if (realizadores.Count == 0)
            {
                Console.WriteLine("Nenhum realizador registado.");
                return;
            }

            foreach (var r in realizadores)
            {
                Console.WriteLine($"ID: {r.Id} | Nome: {r.Nome} | País: {r.Pais}");
            }
        }

        private void MenuRemoverRealizador()
        {
            Console.WriteLine("--- REMOVER REALIZADOR ---");
            Console.Write("Introduza o ID do realizador a remover: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (_realizadorServices.RemoverRealizador(id))
                    Console.WriteLine("\n✅ Sucesso: O realizador foi removido!");
                else
                    Console.WriteLine("\n❌ Erro: ID não encontrado.");
            }
        }
    }
}