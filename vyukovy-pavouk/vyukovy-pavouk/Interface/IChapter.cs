using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IChapter
    {
        public Task <Chapter> GetChapterDetail(int IdChapter);
        public Task CreateChapter(Chapter chapter);
        public Task UpdateChapter(Chapter chapter);  
        public Task DeleteChapter(int IdChapter);

    }
}
