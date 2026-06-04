using MovieManagement.Business.Services;
using MovieManagement.Data.Repositories;
using System;

namespace MovieManagementUi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorio = new FilmeMemoriaRepository();
            var servico = new MovieServices(repositorio);
            var menu = new MovieMenu(servico);
            menu.Exibir();
        }
    }
}