
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;

public interface IUserToken
{
    string GetUserToken();
}
public class UserToken : IUserToken
{
    private const string UserTokenAuth = "userInfo";

    public string GetUserToken()
    {
        return UserTokenAuth;
    }
}

