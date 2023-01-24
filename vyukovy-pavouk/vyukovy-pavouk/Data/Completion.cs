namespace vyukovy_pavouk.Data
{
    public class Completion
{
        public int Id { get; set; }
        public int GroupID { get; set; }
        public int ChapterID { get; set; }
        public List<StudentCompletion> StudentCompletions { get; set; }
    }
}
