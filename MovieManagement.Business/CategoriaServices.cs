using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class CategoriaServices
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaServices(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            if (_categoriaRepository.ObterCategoriaPorNome(categoria.Nome) != null)
            {
                throw new InvalidOperationException("Não é permitido duplicar categorias.");
            }

            _categoriaRepository.AdicionarCategoria(categoria);
        }

        public List<Categoria> ListarCategorias()
        {
            return _categoriaRepository.ListarCategorias();
        }

        public Categoria? ObterCategoriaPorId(int id)
        {
            return _categoriaRepository.ObterCategoriaPorId(id);
        }

        public bool RemoverCategoria(int id)
        {
            return _categoriaRepository.RemoverCategoria(id);
        }
    }
}