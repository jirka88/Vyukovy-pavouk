using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Link
{
        public int Id { get; set; }
        public int ChapterID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public Chapter Chapter { get; set; }
    }
    public class LinkValidator : AbstractValidator<Link>
    {
        public LinkValidator()
        {
            RuleFor(link => link.Reference).NotEmpty().WithMessage("Musíte zadat odkaz!");
            RuleFor(link => link.Reference).MaximumLength(2048).WithMessage("Odkaz je příliš dlouhý!");
            RuleFor(link => link.Name).MaximumLength(15).WithMessage("Název je přiliš dlouhý!");
        }
    }
}
