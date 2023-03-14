using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Group
{
        public int Id { get; set; }
        public string TmGroup { get; set; }
        public Subject Subject { get; set; }
        public ICollection<GroupStudent> GroupStudents { get; set; }
    }
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(group => group.Subject)
                .SetValidator(new SubjectValidator());
        }
    }
}
