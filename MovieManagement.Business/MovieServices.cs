using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Business.Services
{
    public class MovieServices
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IRealizadorRepository _realizadorRepository;

        public MovieServices(
            IFilmeRepository filmeRepository,
            ICategoriaRepository categoriaRepository,
            IRealizadorRepository realizadorRepository)
        {
            _filmeRepository = filmeRepository;
            _categoriaRepository = categoriaRepository;
            _realizadorRepository = realizadorRepository;
        }

        public void AdicionarFilme(Filme filme)
        {
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                throw new System.Exception("O título do filme é obrigatório.");

            _filmeRepository.AdicionarFilme(filme);
        }

        public List<Filme> ListarFilmes()
        {
            var filmes = _filmeRepository.ListarFilmes();

            foreach (var f in filmes)
            {
                if (f.Categoria == null)
                    f.Categoria = _categoriaRepository.ObterCategoriaPorId(f.CategoriaId)!;

                if (f.Realizador == null)
                    f.Realizador = _realizadorRepository.ObterRealizadorPorId(f.RealizadorId)!;
            }

            return filmes;
        }

        public Filme? ObterFilmePorTitulo(string titulo)
        {
            var filme = _filmeRepository.ObterFilmePorTitulo(titulo);

            if (filme != null)
            {
                if (filme.Categoria == null)
                    filme.Categoria = _categoriaRepository.ObterCategoriaPorId(filme.CategoriaId)!;

                if (filme.Realizador == null)
                    filme.Realizador = _realizadorRepository.ObterRealizadorPorId(filme.RealizadorId)!;
            }

            return filme;
        }

        public bool RemoverFilme(int id)
        {
            return _filmeRepository.RemoverFilme(id);
        }

        public void AtualizarFilme(Filme filme)
        {
            if (string.IsNullOrWhiteSpace(filme.Titulo))
                throw new System.Exception("O título do filme não pode ficar vazio.");

            _filmeRepository.AtualizarFilme(filme);
        }
    }
}