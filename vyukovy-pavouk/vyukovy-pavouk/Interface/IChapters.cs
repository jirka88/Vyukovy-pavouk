using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IChapters
{
        public Task <List<Kapitola>> GetChapters(int IdSubject);
        public Task <List<Kapitola>> GetChaptersOnly(int IdSubject);
        public Task <List<KapitolaPrerekvizita>> GetChaptersPrerequisites(int IdSubject);
        public Task CreateCopyOfChapters(List<Kapitola> chapters);
        public Task <List<Kapitola>> GetChaptersWithAll(int IdSubject);

}
}
