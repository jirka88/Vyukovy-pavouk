using Microsoft.Graph;
using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface IGroup
{
        //vrátí skupinu s předmětem
        public Task<Skupina> GetGroup (string IDTeamu);
        //uloží skupinu s předmětem do databáze 
        public Task AddGroup(Skupina skupina);
        //vytvoří uvodní prerekvizitu --> u splnění o hodnotě ID --> 0
        public Task CreateIntroductionPrerequisite(Splneni splneni);
        //vymaže danou skupinu se strukturou 
        public Task DeleteGroup(int Id);
        public Task ResetGroup(int Id);
        public Task ConnectGroup(Skupina group);
    }
}
