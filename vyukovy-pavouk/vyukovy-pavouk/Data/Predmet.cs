using FluentValidation;
namespace vyukovy_pavouk.Data
{
    public class Predmet
{
        public int Id { get; set; }
        public string Nazev { get; set; }
        public string Vytvoril { get; set; }
        public bool Privatni { get; set; }
        public ICollection<Kapitola> Kapitoly { get; set; }
        public int SkupinaID { get; set; }
        public Skupina Skupina { get; set; }
    }
    //Kontrola příchozích dat 
    public class PredmetValidator : AbstractValidator<Predmet>
    {
        public PredmetValidator()
        {
            RuleFor(n => n.Nazev).NotEmpty().WithMessage("Zadejte název předmětu!");
            RuleFor(n => n.Nazev).MaximumLength(30).WithMessage("Předmět má příliš velký název!");
     
        }
    }

}
