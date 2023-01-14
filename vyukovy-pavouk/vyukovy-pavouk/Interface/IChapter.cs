using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IChapter
    {
        public Task <Kapitola> GetKapitolaDetail(int IdKapitoly);
        public Task CreateKapitola(Kapitola kapitola);
        public Task UpdateKapitola(Kapitola kapitola);  
        public Task DeleteKapitola(int IdKapitoly);

    }
}
