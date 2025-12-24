namespace AssetManagement.API.Middleware;

public class InstitutionContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var institutionId = user.FindFirst("institution_id")?.Value;
            if (Guid.TryParse(institutionId, out var parsedId))
            {
                context.Items["InstitutionId"] = parsedId;
            }
        }

        await next(context);
    }
}