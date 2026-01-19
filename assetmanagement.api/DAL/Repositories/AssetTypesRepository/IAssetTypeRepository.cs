using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetTypesRepository;

public interface IAssetTypeRepository : IRepositoryQueryHandler<AssetTypesModel>
{
}