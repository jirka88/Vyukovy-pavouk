using Microsoft.Graph;
using Blazored.SessionStorage;
using Newtonsoft.Json;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;
using vyukovy_pavouk.Interop.TeamsSDK;
using System;
using Microsoft.AspNetCore.Components;

//interface starající se u načtení aktualního Team ID 
public interface IGroupToken
{
    TeamsContext teamsContext { get; set; }
    Task GetGroupAsync(MicrosoftTeams microsoftTeams);

}
public class GroupToken : IGroupToken
{
    public TeamsContext teamsContext { get; set; }
    //získání z SDK informace o MS skupině (ID, Theme...) 
    public async Task GetGroupAsync(MicrosoftTeams microsoftTeams)
    {
        await microsoftTeams.InitializeAsync();
        teamsContext = await microsoftTeams.GetTeamsContextAsync();
    }
}


