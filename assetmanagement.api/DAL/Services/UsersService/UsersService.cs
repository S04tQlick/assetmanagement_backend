using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.UsersRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;
using Serilog;

namespace AssetManagement.API.DAL.Services.UsersService;

public class UsersService(IUserRepository repo, IMapper mapper) : IUsersService 
{
    public async Task<IEnumerable<UsersResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync(
            x => x.Institutions!
        );
        return mapper.Map<IEnumerable<UsersResponse>>(entities);
    }

    public async Task<UsersResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id,
            x => x.Institutions!
        );
        return entity == null ? null : mapper.Map<UsersResponse>(entity);
    }

    public async Task<int> CreateAsync(UsersCreateRequest request)
    {
        if (await repo.ExistsAsync(x=>x.EmailAddress.Equals(request.EmailAddress)))
            throw new ConflictException($"User '{request.EmailAddress}' already exists.");

        var entity = mapper.Map<UsersModel>(request); 
        
        entity.DisplayName  = $"{request.FirstName} {request.LastName}";
        entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
        entity.IsActive = true;

        var created = await repo.CreateAsync(entity);
        return mapper.Map<int>(created);
    }

    public async Task<int> UpdateAsync(Guid id, UsersUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"User with id '{id}' not found.");
    
        // if (await repo.ExistsAsync(x=>x.EmailAddress.Equals(request.EmailAddress)) &&
        //     existing.EmailAddress != request.EmailAddress)
        //     throw new ConflictException($"User '{request.EmailAddress}' already exists.");
    
        mapper.Map(request, existing);
        existing.DisplayName  = $"{request.FirstName} {request.LastName}";
        
        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }
    
    // public async Task<int> UpdateAsync(Guid id, UsersUpdateRequest request)
    // {
    //     var existing = await repo.GetByIdAsync(id)
    //                    ?? throw new NotFoundException($"User with id '{id}' not found.");
    //
    //     // If email address changes → check conflict
    //     if (!string.IsNullOrWhiteSpace(request.EmailAddress) &&
    //         !string.Equals(existing.EmailAddress, request.EmailAddress, StringComparison.OrdinalIgnoreCase) &&
    //         await repo.ExistsAsync(x => x.EmailAddress == request.EmailAddress))
    //     {
    //         throw new ConflictException($"Email '{request.EmailAddress}' already exists.");
    //     }
    //
    //     // Only update fields supplied — secure and clean
    //     if (!string.IsNullOrWhiteSpace(request.FirstName))
    //         existing.FirstName = request.FirstName;
    //
    //     if (!string.IsNullOrWhiteSpace(request.LastName))
    //         existing.LastName = request.LastName;
    //
    //     if (!string.IsNullOrWhiteSpace(request.EmailAddress))
    //         existing.EmailAddress = request.EmailAddress;
    //
    //     if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
    //         existing.PhoneNumber = request.PhoneNumber;
    //
    //     // Update password only if provided
    //     if (!string.IsNullOrWhiteSpace(request.PasswordHash))
    //         existing.PasswordHash = request.PasswordHash;
    //
    //     // Always recompute display name
    //     existing.DisplayName = $"{existing.FirstName} {existing.LastName}".Trim();
    //
    //     var updated = await repo.UpdateAsync(existing);
    //
    //     return updated.Id; // no need to Automap to int unless required
    // }
    

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"User with id '{id}' not found.");

        return await repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<UsersResponse>> GetByInstitutionIdAsync(Guid institutionId)
    {
        var entities = await repo.GetByInstitutionIdAsync(institutionId);
        return mapper.Map<IEnumerable<UsersResponse>>(entities);
    } 

    public async Task<IEnumerable<UsersResponse>> GetActiveUsersAsync()
    {
        var activeEntities = await repo.FindAsync(x => x.IsActive.Equals(true));
        return mapper.Map<IEnumerable<UsersResponse>>(activeEntities);
    } 

    public async Task<UsersResponse?> GetByEmailAsync(string email)
    {
        var entity = await repo.GetByEmailAsync(email);
        return entity == null ? null : mapper.Map<UsersResponse>(entity);
    }

    public HealthResponse GetHealth()
    {
        Log.Information("Health action queried successfully.");
        return new HealthResponse
        {
            Message = ControllerConstants.HealthMessage,
            Timestamp = DateTime.UtcNow
        };
    } 
} 