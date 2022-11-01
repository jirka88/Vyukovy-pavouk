using FluentValidation;
namespace vyukovy_pavouk.Data
{
    public class Predmet
{
        public int Id { get; set; }
        public string Nazev { get; set; }
        public ICollection<Kapitola> Kapitoly { get; set; }
        public List<Skupina> Skupiny { get; set; }
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
