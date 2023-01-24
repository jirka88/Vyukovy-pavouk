using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class ChapterPrerequisite
{
        public int Id { get; set; }
        public int ChapterID  { get; set; }
        public Chapter Chapter { get; set; }
        public int PrerequisiteID { get; set; }      
        public Prerequisite Prerequisite { get; set; }
}
    public class ChapterPrerequisiteValidator : AbstractValidator<ChapterPrerequisite>
    {
        public ChapterPrerequisiteValidator()
        {
            RuleFor(prerekvizita => prerekvizita.Prerequisite.PrerequisiteID).NotEmpty().WithMessage("Prerekvizita nesmí být prázdná!");
        }
    }
}
