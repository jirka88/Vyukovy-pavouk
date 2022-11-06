using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IKapitola
    {
        public Kapitola GetKapitolaDetail(int IdKapitoly);
        public void CreateKapitola(Kapitola kapitola);

    }
}
