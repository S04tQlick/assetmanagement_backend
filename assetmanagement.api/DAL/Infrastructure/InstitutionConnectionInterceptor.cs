using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AssetManagement.API.DAL.Infrastructure;

public class InstitutionConnectionInterceptor(IHttpContextAccessor httpContextAccessor) : DbConnectionInterceptor
{
    public override async Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = default)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext != null && httpContext.Items.TryGetValue("InstitutionId", out var institutionIdObj))
        {
            var institutionId = institutionIdObj?.ToString();
            if (!string.IsNullOrEmpty(institutionId))
            {
                await using var cmd = connection.CreateCommand();
                cmd.CommandText = $"SET app.current_institution = '{institutionId}'";
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
    }
}
