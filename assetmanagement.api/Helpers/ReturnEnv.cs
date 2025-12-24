namespace AssetManagement.API.Helpers;

public static class ReturnHelpers
{
    public static string Env(string recQuery)
    {
        var env = Environment.GetEnvironmentVariable(recQuery) ?? throw new Exception($"{recQuery} missing");
        return env;
    }
}