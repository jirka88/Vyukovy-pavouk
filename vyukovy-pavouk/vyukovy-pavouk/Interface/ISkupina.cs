using Microsoft.Graph;
using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface ISkupina
{
        //vrátí skupinu s předmětem
        public Skupina GetSkupina (string IDTeamu);
        //uloží skupinu s předmětem do databáze 
        public void AddSkupina(Skupina skupina);
        //vytvoří uvodní prerekvizitu --> u splnění o hodnotě ID --> 0
        public void CreateUvodniPrerekvizita(Splneni splneni);
    }
}
