using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Skupina
{
        public int Id { get; set; }
        public string TmSkupina { get; set; }
        public Predmet predmet { get; set; }
        public ICollection<SkupinaStudent> Student { get; set; }
    }
    public class SkupinaValidator : AbstractValidator<Skupina>
    {
        public SkupinaValidator()
        {
            RuleFor(skupina => skupina.predmet)
                .SetValidator(new PredmetValidator());
        }
    }
}
