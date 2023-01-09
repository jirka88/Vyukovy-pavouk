using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface IPredmet
{
        public Task <List<Predmet>> GetSubjects();
        public int GetCountChapters(int IDPredmetu);
        public Task SaveSubject(Predmet predmet);
        public Task EditSubject(Skupina skupina);
        public Task ChangeVisibilitySubject(Predmet predmet);
        public Task<Predmet> GetSubjectWithConnectedGroups(int IDSubject);
        //public Task DeleteSubject(int Id);
    }
}
