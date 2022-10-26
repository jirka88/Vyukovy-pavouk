using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IStudenti
{
        public List<SkupinaStudent> GetStudents(int Id);
        public SkupinaStudent GetStudent(int IdSkupiny, string EmailStudenta);
}
}
