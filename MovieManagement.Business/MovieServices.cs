using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{

    public class MovieServices
    {
        private readonly IFilmeRepository _filmeRepository;

        public MovieServices(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public void AdicionarFilme(Filme novoFilme)
        {
            if (string.IsNullOrWhiteSpace(novoFilme.Titulo))
            {
                throw new Exception ("O título do filme é obrigatório.");
            }

            var filmeExistente = _filmeRepository.ObterFilmePorTitulo(novoFilme.Titulo);
            if (filmeExistente != null)
            {
                throw new Exception("Já existe um filme com esse título.");
            }

            int nota = (int)novoFilme.Classificacao;
            if (nota < 0 || nota > 5)
            {
                throw new Exception("A classificação deve estar entre 0 (Péssimo) e 5 (Excelente).");
            }
            _filmeRepository.AdicionarFilme(novoFilme);
        }

        public List<Filme> ListarFilmes()
        {
            return _filmeRepository.ListarFilmes();
        }

        public Filme? ObterFilmePorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new Exception("O termo de pesquisa não pode estar vazio.");
            }
            return _filmeRepository.ObterFilmePorTitulo(titulo);
        }

        public void RemoverFilme(int id)
        {
            bool foiRemovido = _filmeRepository.RemoverFilme(id);
            if (!foiRemovido)
            {
                throw new Exception("Não foi possível remover: filme não encontrado.");
            }
        }
    }
}
