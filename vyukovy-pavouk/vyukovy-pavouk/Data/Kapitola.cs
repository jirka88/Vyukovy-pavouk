using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace vyukovy_pavouk.Data
{
    public class Kapitola
{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Název { get; set; }
        public string Perex { get; set; }
        [Required]
        public string Kontent { get; set; }
        [Required]
        public int IdPredmetu { get; set; }
        public Predmet predmet { get; set; }

    }
}
