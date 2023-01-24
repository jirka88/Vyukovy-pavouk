using FluentValidation;
namespace vyukovy_pavouk.Data
{
    public class Subject
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public bool Private { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public int GroupID { get; set; }
        public Group Group { get; set; }
    }
    //Kontrola příchozích dat 
    public class SubjectValidator : AbstractValidator<Subject>
    {
        public SubjectValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("Zadejte název předmětu!");
            RuleFor(n => n.Name).MaximumLength(30).WithMessage("Předmět má příliš velký název!");
     
        }
    }

}
