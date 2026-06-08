using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class RealizadorServices
    {
        private readonly IRealizadorRepository _realizadorRepository;

        public RealizadorServices(IRealizadorRepository realizadorRepository)
        {
            _realizadorRepository = realizadorRepository;
        }

        public void AdicionarRealizador(Realizador realizador)
        {
            if (string.IsNullOrWhiteSpace(realizador.Nome))
            {
                throw new ArgumentException("O nome do realizador é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(realizador.Pais))
            {
                throw new ArgumentException("O país do realizador é obrigatório.");
            }

            _realizadorRepository.AdicionarRealizador(realizador);
        }

        public List<Realizador> ListarRealizadores()
        {
            return _realizadorRepository.ListarRealizadores();
        }

        public Realizador? ObterRealizadorPorId(int id)
        {
            return _realizadorRepository.ObterRealizadorPorId(id);
        }

        public bool RemoverRealizador(int id)
        {
            return _realizadorRepository.RemoverRealizador(id);
        }
    }
}