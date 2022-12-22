using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface IPredmet
{
        public List<Predmet> GetSubjects();
        public int GetCountKapitoly(int IDPredmetu);
        public Task SaveSubject(Predmet predmet);
        public Task EditSubject(Skupina skupina);
        public Task ChangeVisibilitySubject(Predmet predmet);
        public void DeleteSubject(int Id);
    }
}
