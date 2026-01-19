using System.Linq.Expressions;
using AssetManagement.API.DAL.QueryHandlers.ServiceQueryHandler;
using AssetManagement.API.DAL.Repositories.VendorsRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.VendorsService;

public class VendorService(IVendorRepository repository, IMapper mapper) : ServiceQueryHandler<VendorsModel, VendorsResponse, VendorsCreateRequest, VendorsUpdateRequest>(repository, mapper), IVendorService
{
    protected override Expression<Func<VendorsModel, object>>[] DefaultIncludes()
    {
        return
        [
            x => x.Institutions!
        ];
    }
    
    protected override Expression<Func<VendorsModel, bool>> IsExistsPredicate(VendorsCreateRequest request)
    {
        return x =>
            x.EmailAddress == request.EmailAddress &&
            x.InstitutionId == request.InstitutionId;
    }
    
    protected override Expression<Func<VendorsModel, bool>> UpdateIsExistsPredicate(Guid id, VendorsUpdateRequest request)
    {
        return x =>
            x.Id != id &&
            x.EmailAddress == request.EmailAddress &&
            x.InstitutionId == request.InstitutionId;
    }
}