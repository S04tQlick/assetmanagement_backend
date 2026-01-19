using AssetManagement.Entities.GeneralResponse; 

namespace AssetManagement.Entities.Models;

[Table("FileUploads")]
public class  FileUploadsModel : BaseModel, IInstitutionOwned
{
    public required string S3Key { get; set; }
    public bool IsLogo { get; set; }
    
    public Guid InstitutionId { get; set; }
    
    [ForeignKey(nameof (InstitutionId))]
    public InstitutionsModel? Institutions { get; set; }  
}