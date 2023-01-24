using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Prerequisite
{
        public int Id { get; set; } 
        public int PrerequisiteID { get; set; }
        public List<ChapterPrerequisite> ChapterPrerequisites { get; set; }
}
    
}
