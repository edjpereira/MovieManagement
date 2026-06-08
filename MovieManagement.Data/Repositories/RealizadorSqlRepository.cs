using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorSqlRepository : IRealizadorRepository
    {
        private readonly AppDbContext _context;

        public RealizadorSqlRepository()
        {
            _context = new AppDbContext();
        }

        public void AdicionarRealizador(Realizador realizador)
        {
            _context.Realizadores.Add(realizador);
            _context.SaveChanges();
        }

        public List<Realizador> ListarRealizadores()
        {
            return _context.Realizadores.ToList();
        }

        public Realizador? ObterRealizadorPorNome(string nome)
        {
            return _context.Realizadores
                .FirstOrDefault(r => r.Nome.ToLower() == nome.ToLower());
        }

        public Realizador? ObterRealizadorPorId(int id)
        {
            return _context.Realizadores.Find(id);
        }

        public bool RemoverRealizador(int id)
        {
            var realizador = _context.Realizadores.Find(id);
            if (realizador == null) return false;

            _context.Realizadores.Remove(realizador);
            _context.SaveChanges();
            return true;
        }
        public void AtualizarRealizador(Realizador realizador)
        {
            _context.Realizadores.Update(realizador);
            _context.SaveChanges();
        }
    }
}