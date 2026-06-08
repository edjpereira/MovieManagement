using MovieManagement.Domain.Enums;

namespace MovieManagement.Domain.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public int Ano { get; set; }
        public string Lingua { get; set; } = null!;
        public Classificacao Classificacao { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public int RealizadorId { get; set; }
        public Realizador Realizador { get; set; } = null!;
    }
}