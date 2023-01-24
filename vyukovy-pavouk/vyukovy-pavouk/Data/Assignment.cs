using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Assignment
{
        public int Id { get; set; }
        public string Reference { get; set; }
        public int ChapterID { get; set; }
        public Chapter Chapter { get; set; }
    }
    public class AssignmentValidator : AbstractValidator<Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(o => o.Reference).NotEmpty().WithMessage("Musíte zadat odkaz na test!");
            RuleFor(o => o.Reference).MaximumLength(2048).WithMessage("Odkaz je příliš dlouhý!");
        }
    }
}
