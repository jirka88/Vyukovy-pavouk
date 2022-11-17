
using FluentValidation;

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
        public List<Zadani> Zadani { get; set; }
        public List<Videa> Videa { get; set; }
        public List<KapitolaPrerekvizita> KapitolaPrerekvizita { get; set; }

    }
    public class KapitolaValidator : AbstractValidator<Kapitola>
    {
        public KapitolaValidator()
        {
            RuleFor(kapitola => kapitola.Název).NotEmpty().WithMessage("Kapitola musí mít název!");
            RuleFor(kapitola => kapitola.Název).MaximumLength(30).WithMessage("Název kapitoly je příliš dlouhý!");
            RuleFor(kapitola => kapitola.Perex).MaximumLength(255).WithMessage("Perex je příliš dlouhý!");
            RuleFor(kapitola => kapitola.Kontent).NotEmpty().WithMessage("Kapitola musí mít obsah!");
            RuleFor(kapitola => kapitola.Kontent).MaximumLength(65535).WithMessage("Obsah dosáhl maximální délky!");
            RuleForEach(kapitola => kapitola.Zadani)
                .SetValidator(new ZadaniValidator());
            RuleForEach(kapitola => kapitola.Videa)
                .SetValidator(new VideaValidator());
        }
    }
}
