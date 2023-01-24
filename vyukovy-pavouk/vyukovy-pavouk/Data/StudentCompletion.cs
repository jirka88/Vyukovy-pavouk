namespace vyukovy_pavouk.Data
{
    public class StudentCompletion
{
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int CompletionID { get; set; }
        public Completion Completion { get; set; }
        public bool Success { get; set; }

}
}
