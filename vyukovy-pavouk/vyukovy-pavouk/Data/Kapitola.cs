
namespace vyukovy_pavouk.Data
{
    public class Kapitola
{
        public int Id { get; set; }
        public string Název { get; set; }
        public string Perex { get; set; }
        public string Kontent { get; set; }
        public int IdPredmetu { get; set; }
        public Predmet predmet { get; set; }
        public ICollection<Zadani> Zadani { get; set; }
        public ICollection<Videa> Videa { get; set; }

    }
}
