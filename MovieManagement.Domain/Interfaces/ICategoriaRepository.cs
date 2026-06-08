using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        void AdicionarCategoria(Categoria categoria);
        List<Categoria> ListarCategorias();
        Categoria? ObterCategoriaPorNome(string nome);
        Categoria? ObterCategoriaPorId(int id);
        bool RemoverCategoria(int id);
        void AtualizarCategoria(Categoria categoria);
    }
}