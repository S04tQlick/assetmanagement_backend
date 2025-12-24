using System.Reflection;

namespace AssetManagement.API.Constants;

public static class ControllerConstants
{
    public const string HealthRoute = "health";
    public const string GetByDateRoute = "{date}";
    public const string GetByIdRoute = "{rowId}/{parentId}";
    public static string HealthMessage =>
        $"Application is running healthy from {Assembly.GetExecutingAssembly().GetName().Name}.";
}