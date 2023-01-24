using Microsoft.Graph;
using vyukovy_pavouk.Data;
namespace vyukovy_pavouk.Interface
{
    public interface IGroup
{
        //vrátí skupinu s předmětem
        public Task<Data.Group> GetGroup (string IDTeam);
        //uloží skupinu s předmětem do databáze 
        public Task AddGroup(Data.Group group);
        //vytvoří uvodní prerekvizitu --> u splnění o hodnotě ID --> 0
        public Task CreateIntroductionPrerequisite(Completion splneni);
        //vymaže danou skupinu se strukturou 
        public Task DeleteGroup(int Id);
        public Task ResetGroup(int Id);
        public Task ConnectGroup(Data.Group group);
    }
}
