using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface ISubject
{
        public Task <List<Predmet>> GetSubjects();
        public int GetCountChapters(int IdSubject);
        public Task SaveSubject(Predmet Subject);
        public Task EditSubject(Skupina group);
        public Task ChangeVisibilitySubject(Predmet subject);
        public Task<Predmet> GetSubjectWithConnectedGroups(int IdSubject);
        //public Task DeleteSubject(int Id);
    }
}
