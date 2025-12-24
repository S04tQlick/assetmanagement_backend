namespace AssetManagement.API.DAL.Services.InstitutionContextService;

public class InstitutionContextService : IInstitutionContextService
{
    public Guid? InstitutionId { get; }

    public InstitutionContextService(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;
        
        if (httpContext == null || !httpContext.Items.TryGetValue("InstitutionId", out var value)) return;
        
        if (Guid.TryParse(value?.ToString(), out var id))
        {
            InstitutionId = id;
        }
    }
}