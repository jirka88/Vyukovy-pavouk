using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IKapitola
{
        public List<Kapitola> GetKapitoly(int IdPredmetu);
}
}
