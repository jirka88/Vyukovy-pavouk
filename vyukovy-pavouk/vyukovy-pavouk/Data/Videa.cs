using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Videa
{
        public int Id { get; set; }
        public int KapitolaID { get; set; }
        public string Nazev { get; set; }
        public string Odkaz { get; set; }
        public Kapitola kapitola { get; set; }
    }
    public class VideaValidator : AbstractValidator<Videa>
    {
        public VideaValidator()
        {
            RuleFor(videa => videa.Odkaz).NotEmpty().WithMessage("Musíte zadat odkaz!");
            RuleFor(videa => videa.Odkaz).MaximumLength(2048).WithMessage("Odkaz je příliš dlouhý!");
            RuleFor(videa => videa.Nazev).MaximumLength(15).WithMessage("Název je přiliš dlouhý!");
        }
    }
}
