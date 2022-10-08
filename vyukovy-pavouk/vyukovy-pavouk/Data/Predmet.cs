using System.ComponentModel.DataAnnotations;

namespace vyukovy_pavouk.Data
{
    public class Predmet
{
        [Key]
        public int Id { get; set; }
        public string Nazev { get; set; }

        public List<Kapitola> Kapitoly { get; set; }
}
}
