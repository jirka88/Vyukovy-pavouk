using Microsoft.Graph;
using Blazored.SessionStorage;
using Newtonsoft.Json;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;
using vyukovy_pavouk.Interop.TeamsSDK;
using System;
using Microsoft.AspNetCore.Components;

public interface IGroupToken
{
    TeamsContext teamsContext { get; set; }
    Task GetGroupAsync(MicrosoftTeams microsoftTeams);

}
public class GroupToken : IGroupToken
{
    public TeamsContext teamsContext { get; set; }
    public async Task GetGroupAsync(MicrosoftTeams microsoftTeams)
    {
        await microsoftTeams.InitializeAsync();
        teamsContext = await microsoftTeams.GetTeamsContextAsync();
    }
}


