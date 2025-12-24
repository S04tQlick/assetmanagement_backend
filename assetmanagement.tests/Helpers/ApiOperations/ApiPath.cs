namespace AssetManagement.Tests.Helpers.ApiOperations;

public static class ApiPath
{
    public static string SetAssetTypesControllerRoute(string? route = "") =>
        $"api/AssetTypes/{route}";
    
    public static string SetAssetCategoriesControllerRoute(string? route = "") =>
        $"api/AssetCategories/{route}";
    
    public static string SetInstitutionsControllerRoute(string? route = "") =>
        $"api/Institutions/{route}";
    
    public static string SetBranchesControllerRoute(string? route = "") =>
        $"api/Branches/{route}";
    
    public static string SetRolesControllerRoute(string? route = "") =>
        $"api/Roles/{route}"; 
    
    public static string SetVendorsControllerRoute(string? route = "") =>
        $"api/Vendors/{route}";
    
    public static string SetAssetsControllerRoute(string? route = "") =>
        $"api/Assets/{route}";
    
    public static string SetUsersControllerRoute(string? route = "") =>
        $"api/Users/{route}";
    
    public static string SetUserRolesControllerRoute(string? route = "") =>
        $"api/UserRoles/{route}";
}