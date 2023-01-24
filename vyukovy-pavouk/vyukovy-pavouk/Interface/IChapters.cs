using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IChapters
{
        public Task <List<Chapter>> GetChapters(int IdSubject);
        public Task <List<Chapter>> GetChaptersOnly(int IdSubject);
        public Task <List<ChapterPrerequisite>> GetChaptersPrerequisites(int IdSubject);
        public Task CreateCopyOfChapters(List<Chapter> chapters);
        public Task <List<Chapter>> GetChaptersWithAll(int IdSubject);

}
}
