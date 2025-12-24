namespace AssetManagement.Entities.GeneralResponse;

public class BaseActionResponse<T>
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public T? Data { get; set; }
    
    
    //public int RowCount { get; set; }
    
    // Optionally include navigation collections if needed
    //public int UserCount { get; set; }
    //public int BranchCount { get; set; }
    //public int AssetCount { get; set; }
}