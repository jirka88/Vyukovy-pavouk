using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class KapitolaPrerekvizita
{
        public int Id { get; set; }
        public int KapitolaId  { get; set; }
        public Kapitola kapitola { get; set; }
        public int IdPrerekvizita { get; set; }      
        public Prerekvizity prerekvizita { get; set; }
}
    public class KapitolaPrerekvizitaValidator : AbstractValidator<KapitolaPrerekvizita>
    {
        public KapitolaPrerekvizitaValidator()
        {
            RuleFor(prerekvizita => prerekvizita.prerekvizita.IdPrerekvizity).NotEmpty().WithMessage("Prerekvizita nesmí být prázdná!");
        }
    }
}
