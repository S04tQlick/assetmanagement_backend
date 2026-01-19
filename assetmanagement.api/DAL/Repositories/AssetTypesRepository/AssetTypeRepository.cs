using System.Linq.Expressions;
using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.AssetTypesRepository;

public class  AssetTypeRepository(ApplicationDbContext ctx) : RepositoryQueryHandler<AssetTypesModel>(ctx), IAssetTypeRepository
{
}