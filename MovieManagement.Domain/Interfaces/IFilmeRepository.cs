using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        void AdicionarFilme(Filme filme);
        List<Filme> ListarFilmes();
        Filme? ObterFilmePorTitulo(string titulo);
        bool RemoverFilme(int id);
        void AtualizarFilme(Filme filme);
    }
}