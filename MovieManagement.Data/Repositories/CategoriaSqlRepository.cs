using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaSqlRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaSqlRepository()
        {
            _context = new AppDbContext();
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public List<Categoria> ListarCategorias()
        {
            return _context.Categorias.ToList();
        }

        public Categoria? ObterCategoriaPorNome(string nome)
        {
            return _context.Categorias
                .FirstOrDefault(c => c.Nome.ToLower() == nome.ToLower());
        }

        public Categoria? ObterCategoriaPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public bool RemoverCategoria(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return false;

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return true;
        }
        public void AtualizarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }
    }
}