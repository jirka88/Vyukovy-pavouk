
using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Perex { get; set; }
        public string Content { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Link> Links { get; set; }
        public List<ChapterPrerequisite> ChapterPrerequisites { get; set; }

    }
    public class ChapterValidator : AbstractValidator<Chapter>
    {
        public ChapterValidator()
        {
            RuleFor(chapter => chapter.Name).NotEmpty().WithMessage("Kapitola musí mít název!");
            RuleFor(chapter => chapter.Name).MaximumLength(30).WithMessage("Název kapitoly je příliš dlouhý!");
            RuleFor(chapter => chapter.Perex).MaximumLength(255).WithMessage("Perex je příliš dlouhý!");
            RuleFor(chapter => chapter.Content).NotEmpty().WithMessage("Kapitola musí mít obsah!");
            RuleFor(chapter => chapter.Content).MaximumLength(65535).WithMessage("Obsah dosáhl maximální délky!");
            RuleForEach(chapter => chapter.Assignments)
                .SetValidator(new AssignmentValidator());
            RuleForEach(chapter => chapter.Links)
                .SetValidator(new LinkValidator());
            RuleForEach(chapter => chapter.ChapterPrerequisites)
                .SetValidator(new ChapterPrerequisiteValidator());
        }
    }
}
