using AssetManagement.Entities.Models;

namespace AssetManagement.Entities.GeneralResponse;

public interface IInstitutionOwned
{
    Guid InstitutionId { get; }
    public InstitutionsModel? Institutions { get; set; }
}