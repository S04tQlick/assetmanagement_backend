using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.VendorsRepository;

public class VendorRepository(ApplicationDbContext ctx) : RepositoryQueryHandler<VendorsModel>(ctx), IVendorRepository
{
    // Add vendor-specific methods here
}





// public class VendorRepository(ApplicationDbContext ctx) : RepositoryQueryHandler<VendorsModel>(ctx), IVendorRepository
//
//
//
//
//
//  {
//      private readonly ApplicationDbContext _context = context;
//
//      public async Task<VendorsModel?> GetByNameAsync(string name, Guid institutionId) =>
//          await _context.VendorsModel
//              .FirstOrDefaultAsync(b => string.Equals(b.VendorsName, name, StringComparison.OrdinalIgnoreCase)
//                                        && b.InstitutionId == institutionId);
//
//      public async Task<IEnumerable<VendorsModel>> GetByInstitutionIdAsync(Guid institutionId) =>
//          await _context.VendorsModel
//              .Where(b => b.InstitutionId == institutionId)
//              .ToListAsync();
//  }