using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaMemoriaRepository : ICategoriaRepository
    {
        private readonly List<Categoria> _categorias = new List<Categoria>();
        private int proximoId = 1;

        public void AdicionarCategoria(Categoria categoria)
        {
            categoria.Id = _categorias.Count == 0 ? 1 : _categorias.Max(c => c.Id) + 1;
            _categorias.Add(categoria);
        }

        public List<Categoria> ListarCategorias()
        {
            return new List<Categoria>(_categorias);
        }

        public Categoria? ObterCategoriaPorNome(string nome)
        {
            return _categorias.FirstOrDefault(c => c.Nome.Equals(nome, System.StringComparison.OrdinalIgnoreCase));
        }

        public Categoria? ObterCategoriaPorId(int id)
        {
            return _categorias.FirstOrDefault(c => c.Id == id);
        }

        public bool RemoverCategoria(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria != null)
            {
                _categorias.Remove(categoria);
                return true;
            }
            return false;
        }
    }
}