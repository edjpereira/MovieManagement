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
                Console.WriteLine("3. Editar realizador");
                Console.WriteLine("4. Remover realizador");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        MenuAdicionarRealizador();
                        break;
                    case 2: 
                        MenuListarRealizadores();
                        break;
                    case 3:
                        MenuEditarRealizador();
                        break;
                    case 4:
                        MenuRemoverRealizador();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!"); 
                        break;
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

        private void MenuEditarRealizador()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("              EDITAR REALIZADOR                  ");
            Console.WriteLine("=================================================");

            var listaRealizadores = _realizadorServices.ListarRealizadores();

            if (listaRealizadores.Count == 0)
            {
                Console.WriteLine("\n⚠️ Não existem realizadores registados.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nRealizadores no Sistema:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var r in listaRealizadores)
            {
                Console.WriteLine($"[ID: {r.Id}] - {r.Nome} ({r.Pais})");
            }
            Console.WriteLine("-------------------------------------------------\n");

            Console.Write("Digite o ID do realizador que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                Console.ReadKey();
                return;
            }

            var realizador = listaRealizadores.FirstOrDefault(r => r.Id == id);
            if (realizador == null)
            {
                Console.WriteLine("❌ Realizador não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n✍️ A editar: {realizador.Nome}");
            Console.WriteLine("(Pressione ENTER sem digitar nada para MANTER o valor atual)\n");

            // 1. EDITAR NOME
            Console.Write($"Novo Nome [{realizador.Nome}]: ");
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                realizador.Nome = novoNome;
            }

            // 2. EDITAR PAÍS
            Console.Write($"Novo País/Origem [{realizador.Pais}]: ");
            string novoPais = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoPais))
            {
                realizador.Pais = novoPais;
            }

            try
            {
                _realizadorServices.AtualizarRealizador(realizador);
                Console.WriteLine("\n✅ Realizador atualizado com sucesso!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\n❌ Erro ao atualizar: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private void MenuRemoverRealizador()
        {
            Console.WriteLine("--- REMOVER REALIZADOR ---");
            var listaRealizadores = _realizadorServices.ListarRealizadores();

            if (listaRealizadores.Count == 0)
            {
                Console.WriteLine("\n⚠️ Não existem realizadores registados.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nRealizadores no Sistema:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var r in listaRealizadores)
            {
                Console.WriteLine($"[ID: {r.Id}] - {r.Nome} ({r.Pais})");
            }
            Console.WriteLine("-------------------------------------------------\n");
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