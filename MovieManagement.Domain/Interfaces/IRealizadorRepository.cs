using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface IRealizadorRepository
    {
        void AdicionarRealizador(Realizador realizador);
        List<Realizador> ListarRealizadores();
        Realizador? ObterRealizadorPorNome(string nome);
        Realizador? ObterRealizadorPorId(int id);
        bool RemoverRealizador(int id);
    }
}