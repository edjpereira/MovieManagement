using MovieManagement.Domain.Enums;

namespace MovieManagement.Domain.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Lingua { get; set; }
        public Classificacao Classificacao { get; set; }
    }
}