using AssetManagement.API.DAL.Repositories.AddressesRepository;
using AssetManagement.API.Exceptions;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using AutoMapper;

namespace AssetManagement.API.DAL.Services.AddressesService;

public class AddressesService(IAddressRepository repo, IMapper mapper) : IAddressesService
{
    public async Task<IEnumerable<AddressesResponse>> GetAllAsync()
    {
        var entities = await repo.GetAllAsync();
        return mapper.Map<IEnumerable<AddressesResponse>>(entities);
    }

    public async Task<AddressesResponse?> GetByIdAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        return entity == null ? null : mapper.Map<AddressesResponse>(entity);
    } 

    public async Task<int> CreateAsync(AddressesCreateRequest request)
    {
        if (await repo.ExistsAsync(x => x.Street == request.Street && x.PostalCode == request.PostalCode))
            throw new ConflictException($"Address '{request.Street}, {request.PostalCode}' already exists.");

        var entity = mapper.Map<AddressesModel>(request);
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        var created = await repo.CreateAsync(entity);
        return await mapper.Map<Task<int>>(created);
    } 
    
    public async Task<int> UpdateAsync(Guid id, AddressesUpdateRequest request)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException($"Address with id '{id}' not found."); 
        
        if (await repo.ExistsAsync(x => x.Street == request.Street && x.PostalCode == request.PostalCode && x.Id != id))
            throw new ConflictException($"Address '{request.Street}, {request.PostalCode}' already exists.");

        mapper.Map(request, existing);
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await repo.UpdateAsync(existing);
        return mapper.Map<int>(updated);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException($"Address with id '{id}' not found.");

        return await repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<AddressesResponse>> GetByCityAsync(string city)
    {
        var entities = await repo.GetByCityAsync(city);
        return  mapper.Map<IEnumerable<AddressesResponse>>(entities);
    }

    public async Task<IEnumerable<AddressesResponse>> GetByCountryAsync(string county)
    {
        var entities = await repo.GetByCityAsync(county);
        return  mapper.Map<IEnumerable<AddressesResponse>>(entities);
    }

    public async Task<IEnumerable<AddressesResponse>> GetByRegionAsync(string region)
    {
        var entities = await repo.GetByCityAsync(region);
        return  mapper.Map<IEnumerable<AddressesResponse>>(entities);
    }
}