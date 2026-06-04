using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class FilmeMemoriaRepository : IFilmeRepository
    {
        private readonly List<Filme> _filmes = new List<Filme>();
        private int proximoId = 1;

        public void AdicionarFilme(Filme filme)
        {
            filme.Id = proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ListarFilmes()
        {
            return new List<Filme>(_filmes);
        }

        public Filme? ObterFilmePorTitulo(string titulo)
        {
            return _filmes.FirstOrDefault(f => f.Titulo.Equals(titulo, System.StringComparison.OrdinalIgnoreCase));
        }

        public bool RemoverFilme(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                _filmes.Remove(filme);
                return true;
            }
            return false;
        }
    }
}