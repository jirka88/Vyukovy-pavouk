using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IKapitoly
{
        public Task <List<Kapitola>> GetChapters(int IdPredmetu);
        public Task <List<Kapitola>> GetChaptersOnly(int IdPredmetu);
        public Task <List<KapitolaPrerekvizita>> GetChaptersPrerequisites(int IdPredmetu);
        public Task CreateCopyOfChapters(List<Kapitola> chapters);
        public Task <List<Kapitola>> GetChaptersWithAll(int IdPredmetu);

}
}
