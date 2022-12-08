using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface IPredmet
{
        public List<Predmet> GetPredmety();
        public int GetCountKapitoly(int IDPredmetu);
        public void SavePredmet(Predmet predmet);
        public void UpravPredmet(Skupina skupina);
        public void SmazPredmet(int Id);
    }
}
