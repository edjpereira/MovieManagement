using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using MovieManagement.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieManagement.Data.Repositories
{
    public class FilmeSqlRepository : IFilmeRepository
    {
        private readonly AppDbContext _context;

        public FilmeSqlRepository()
        {
            _context = new AppDbContext();
        }

        public void AdicionarFilme(Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
        }

        public List<Filme> ListarFilmes()
        {
            return _context.Filmes
            .Include(f => f.Categoria)
            .Include(f => f.Realizador)
            .ToList();
        }

        public Filme? ObterFilmePorTitulo(string titulo)
        {
            return _context.Filmes
            .FirstOrDefault(f => f.Titulo.ToLower() == titulo.ToLower());
        }

        public bool RemoverFilme(int id)
        {
            var filme = _context.Filmes.Find(id);

            if (filme == null)
            {
                return false;
            }

            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return true;
        }
    }
}