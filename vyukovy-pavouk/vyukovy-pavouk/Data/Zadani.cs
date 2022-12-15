using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Zadani
{
        public int Id { get; set; }
        public string Odkaz { get; set; }
        public int KapitolaID { get; set; }
        public Kapitola kapitola { get; set; }
    }
    public class ZadaniValidator : AbstractValidator<Zadani>
    {
        public ZadaniValidator()
        {
            RuleFor(o => o.Odkaz).NotEmpty().WithMessage("Musíte zadat odkaz na test!");
            RuleFor(o => o.Odkaz).MaximumLength(2048).WithMessage("Odkaz je příliš dlouhý!");
        }
    }
}
