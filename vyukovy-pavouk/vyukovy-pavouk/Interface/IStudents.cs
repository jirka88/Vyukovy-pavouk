using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IStudents
{
        public List<GroupStudent> GetStudents(int Id);
        public Student GetStudentProgres(int Id, int IdGroup);
        public Student GetStudent(int IdGroup, string EmailStudent);
        public Task CreateNewStudent(Student student);
        public Task CreateNewConnect(GroupStudent groupStudent);
        public Task DeleteStudent(int IdStudent, int IdGroup);
        public Task CreateSplneni(StudentCompletion studentSplneni);
        public Task OpravaSplneni(StudentCompletion studentSplneni);
}
}
