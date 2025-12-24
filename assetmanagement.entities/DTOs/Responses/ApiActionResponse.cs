namespace AssetManagement.Entities.DTOs.Responses;

public class ApiActionResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}