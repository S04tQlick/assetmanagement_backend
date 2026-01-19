namespace AssetManagement.Entities.Models;

public class AwsSettingsModel
{
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string Region { get; set; }
    public required string BucketName { get; set; }
    public required string BucketLogoDirectory { get; set; }
    public required string BucketDocumentDirectory { get; set; }
}
