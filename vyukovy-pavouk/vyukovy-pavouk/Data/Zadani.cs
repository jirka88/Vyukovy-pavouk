using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Zadani
{
        public int Id { get; set; }
        public string Odkaz { get; set; }
        public int IdKapitoly { get; set; }
        public Kapitola kapitola { get; set; }
    }
    public class ZadaniValidator : AbstractValidator<Zadani>
    {
        public ZadaniValidator()
        {
            RuleFor(n => n.Odkaz).NotEmpty().WithMessage("Musíte zadat test!");
        }
    }
}
