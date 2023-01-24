using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface ISubject
{
        public Task <List<Subject>> GetSubjects();
        public int GetCountChapters(int IdSubject);
        public Task SaveSubject(Subject Subject);
        public Task EditSubject(Group group);
        public Task ChangeVisibilitySubject(Subject subject);
        public Task<Subject> GetSubjectWithConnectedGroups(int IdSubject);
        //public Task DeleteSubject(int Id);
    }
}
