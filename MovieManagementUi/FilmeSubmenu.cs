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
                Console.WriteLine("4. Editar Filme");
                Console.WriteLine("5. Remover filme");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

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
                        MenuEditarFilme();
                        break;
                    case 5:
                        MenuRemoverFilme();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
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
            foreach (var cat in categorias)
            {
                Console.WriteLine($"[{cat.Id}] {cat.Nome}");
            }
            Console.WriteLine("[0] -> Criar Nova Categoria");

            int categoriaId;
            while (true)
            {
                Console.Write("Escolha o ID da Categoria (ou 0 para criar): ");
                if (int.TryParse(Console.ReadLine(), out categoriaId))
                {
                    if (categoriaId == 0 || categorias.Any(c => c.Id == categoriaId))
                    {
                        break;
                    }
                }
                Console.WriteLine("❌ Categoria inválida. Escolha um ID da lista ou 0.");
            }

            if (categoriaId == 0)
            {
                Console.WriteLine("\n--- Criar Nova Categoria No Momento ---");
                string nomeNovaCat;
                while (true)
                {
                    Console.Write("Nome da nova categoria: ");
                    nomeNovaCat = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(nomeNovaCat)) break;
                    Console.WriteLine("❌ O nome da categoria não pode estar vazio.");
                }

                var novaCategoria = new Categoria { Nome = nomeNovaCat };

                _categoriaServices.AdicionarCategoria(novaCategoria);

                categoriaId = novaCategoria.Id;
                Console.WriteLine($"\n✅ Categoria '{nomeNovaCat}' criada com sucesso (ID: {categoriaId}) e associada ao filme!");
            }

            novoFilme.CategoriaId = categoriaId;

            var realizadores = _realizadorServices.ListarRealizadores();
            Console.WriteLine("\n--- Realizadores Disponíveis ---");
            foreach (var real in realizadores)
            {
                Console.WriteLine($"[{real.Id}] {real.Nome} ({real.Pais})");
            }
            Console.WriteLine("[0] -> Criar Novo Realizador"); // Atalho visual

            int realizadorId;
            while (true)
            {
                Console.Write("Escolha o ID do Realizador (ou 0 para criar): ");
                if (int.TryParse(Console.ReadLine(), out realizadorId))
                {
                    if (realizadorId == 0 || realizadores.Any(r => r.Id == realizadorId))
                    {
                        break;
                    }
                }
                Console.WriteLine("❌ Realizador inválido. Escolha um ID da lista ou 0.");
            }

            if (realizadorId == 0)
            {
                Console.WriteLine("\n--- Criar Novo Realizador No Momento ---");
                string nomeNovoReal, paisNovoReal;

                while (true)
                {
                    Console.Write("Nome do realizador: ");
                    nomeNovoReal = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(nomeNovoReal)) break;
                    Console.WriteLine("❌ O nome não pode estar vazio.");
                }

                Console.Write("País de origem: ");
                paisNovoReal = Console.ReadLine()?.Trim() ?? "Desconhecido";

                var novoRealizador = new Realizador { Nome = nomeNovoReal, Pais = paisNovoReal };
                _realizadorServices.AdicionarRealizador(novoRealizador);

                realizadorId = novoRealizador.Id;
                Console.WriteLine($"\n✅ Realizador '{nomeNovoReal}' criado com sucesso (ID: {realizadorId}) e associado ao filme!");
            }

            novoFilme.CategoriaId = categoriaId;
            novoFilme.RealizadorId = realizadorId;

            novoFilme.Categoria = null!;
            novoFilme.Realizador = null!;

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

        private void MenuEditarFilme()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                EDITAR FILME                     ");
            Console.WriteLine("=================================================");

            var listaParaEdicao = _movieServices.ListarFilmes();

            if (listaParaEdicao.Count == 0)
            {
                Console.WriteLine("\n⚠️ Não existem filmes registados no sistema.");
                Console.WriteLine("\nPressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nFilmes no Sistema:");
            Console.WriteLine("-------------------------------------------------");
            foreach (var f in listaParaEdicao)
            {
                Console.WriteLine($"[ID: {f.Id}] - {f.Titulo}");
            }
            Console.WriteLine("-------------------------------------------------\n");
            Console.Write("Introduza o ID do filme que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                Console.ReadKey();
                return;
            }

            var filmes = _movieServices.ListarFilmes();
            var filme = listaParaEdicao.FirstOrDefault(f => f.Id == id);

            if (filme == null)
            {
                Console.WriteLine("❌ Filme não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n✍️ A editar: {filme.Titulo} ({filme.Ano})");
            Console.WriteLine("(Pressione ENTER sem escrever nada para MANTER o valor atual)\n");

            // Editar título
            Console.Write($"Novo Título [{filme.Titulo}]: ");
            string novoTitulo = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoTitulo))
            {
                filme.Titulo = novoTitulo;
            }

            // Editar ano
            Console.Write($"Novo Ano [{filme.Ano}]: ");
            string anoInput = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(anoInput) && int.TryParse(anoInput, out int novoAno))
            {
                filme.Ano = novoAno;
            }

            // Editar língua
            Console.Write($"Nova Língua [{filme.Lingua}]: ");
            string novaLingua = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaLingua))
            {
                filme.Lingua = novaLingua;
            }

            // 4. Editar categoria
            Console.WriteLine($"\nCategoria Atual: {filme.Categoria?.Nome ?? "N/A"}");
            string respostaCat = "";
            while (respostaCat != "S" && respostaCat != "N")
            {
                Console.Write("Deseja alterar a categoria? (S/N): ");
                respostaCat = Console.ReadLine()?.Trim().ToUpper() ?? "";
                if (respostaCat != "S" && respostaCat != "N")
                {
                    Console.WriteLine("❌ Opção inválida! Digite apenas 'S' para Sim ou 'N' para Não.");
                }
            }

            if (respostaCat == "S")
            {
                Console.WriteLine("\nCategorias Disponíveis:");
                var categorias = _categoriaServices.ListarCategorias();
                foreach (var cat in categorias)
                {
                    Console.WriteLine($"{cat.Id} - {cat.Nome}");
                }
                Console.Write("Escolha o ID da nova Categoria: ");
                if (int.TryParse(Console.ReadLine(), out int novaCatId))
                {
                    filme.CategoriaId = novaCatId;
                    filme.Categoria = null!;
                }
            }

            // Editar realizador
            Console.WriteLine($"\nRealizador Atual: {filme.Realizador?.Nome ?? "N/A"}");
            string respostaReal = "";
            while (respostaReal != "S" && respostaReal != "N")
            {
                Console.Write("Deseja alterar o realizador? (S/N): ");
                respostaReal = Console.ReadLine()?.Trim().ToUpper() ?? "";
                if (respostaReal != "S" && respostaReal != "N")
                {
                    Console.WriteLine("❌ Opção inválida! Digite apenas 'S' para Sim ou 'N' para Não.");
                }
            }

            if (respostaReal == "S")
            {
                Console.WriteLine("\nRealizadores Disponíveis:");
                var realizadores = _realizadorServices.ListarRealizadores();
                foreach (var real in realizadores)
                {
                    Console.WriteLine($"{real.Id} - {real.Nome}");
                }
                Console.Write("Escolha o ID do novo Realizador: ");
                if (int.TryParse(Console.ReadLine(), out int novoRealId))
                {
                    filme.RealizadorId = novoRealId;
                    filme.Realizador = null!;
                }
            }

            // Guardar
            try
            {
                _movieServices.AtualizarFilme(filme);
                Console.WriteLine("\n✅ Filme atualizado com sucesso no sistema!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"\n❌ Erro ao atualizar: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
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