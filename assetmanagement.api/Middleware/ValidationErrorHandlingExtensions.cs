using AssetManagement.Entities.DTOs.Errors.ErrorRequests;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Middleware;

public static class ValidationErrorHandlingExtensions
{
    public static IServiceCollection AddCustomValidationResponses(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                var errorResponse = new ValidationErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Validation failed for one or more fields.",
                    Errors = errors,
                    Path = context.HttpContext.Request.Path
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}