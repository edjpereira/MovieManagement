using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorMemoriaRepository : IRealizadorRepository
    {
        private readonly List<Realizador> _realizadores = new List<Realizador>();
        private int proximoId = 1;

        public void AdicionarRealizador(Realizador realizador)
        {
            realizador.Id = _realizadores.Count == 0 ? 1 : _realizadores.Max(c => c.Id) + 1;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ListarRealizadores()
        {
            return new List<Realizador>(_realizadores);
        }

        public Realizador? ObterRealizadorPorNome(string nome)
        {
            return _realizadores.FirstOrDefault(r => r.Nome.Equals(nome, System.StringComparison.OrdinalIgnoreCase));
        }

        public Realizador? ObterRealizadorPorId(int id)
        {
            return _realizadores.FirstOrDefault(r => r.Id == id);
        }

        public bool RemoverRealizador(int id)
        {
            var realizador = _realizadores.FirstOrDefault(r => r.Id == id);
            if (realizador != null)
            {
                _realizadores.Remove(realizador);
                return true;
            }
            return false;
        }
    }
}