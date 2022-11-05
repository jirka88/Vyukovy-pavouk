using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IKapitoly
{
        public List<Kapitola> GetKapitoly(int IdPredmetu);
        public List<Kapitola> GetKapitolyOnly(int IdPredmetu);

}
}
