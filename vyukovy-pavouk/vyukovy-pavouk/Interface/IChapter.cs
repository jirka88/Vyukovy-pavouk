using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IChapter
    {
        public Task <Kapitola> GetKapitolaDetail(int IdChapter);
        public Task CreateKapitola(Kapitola chapter);
        public Task UpdateKapitola(Kapitola chapter);  
        public Task DeleteKapitola(int IdChapter);

    }
}
