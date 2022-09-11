
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;
using Newtonsoft.Json;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;
using System;


public interface IUserToken
{
    User Profile { get; set; }

    Task GetUserAsync(IIWSessionStorageService sessionStorage);

    Task SetLocalUser(IIWSessionStorageService sessionStorage);
}
public class UserToken : IUserToken
{
    private const string UserTokenAuth = "userInfo";

    public User Profile { get; set; } = new User(); 

    public async Task GetUserAsync(IIWSessionStorageService sessionStorage)
    {
        string json = await sessionStorage.GetItemAsync<string>(UserTokenAuth);
        if (json != null)
        {
            Profile = JsonConvert.DeserializeObject<User>(json);
        }
        else {
            Profile = null;
        }
        
    }
    public async Task SetLocalUser(IIWSessionStorageService sessionStorage)
    {
        string json = JsonConvert.SerializeObject(Profile);
        await sessionStorage.SetItemAsync<string>(UserTokenAuth, json);
    }
}

