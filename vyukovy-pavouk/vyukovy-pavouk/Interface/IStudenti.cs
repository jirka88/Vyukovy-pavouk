using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IStudenti
{
        public List<SkupinaStudent> GetStudents(int Id);
        public Student GetStudent(int IdSkupiny, string EmailStudenta);
        public void CreateNewStudent(Student student);
        public void CreateNewConnect(SkupinaStudent skupinaStudent);
        public void CreateNewConnectPrerekvizita(StudentSplneni splneni);

}
}
