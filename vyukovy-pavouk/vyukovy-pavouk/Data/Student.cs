namespace vyukovy_pavouk.Data
{
    public class Student
{
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<GroupStudent> GroupStudents { get; set; }
        public List<StudentCompletion> StudentCompletion { get; set; }

    }
}
