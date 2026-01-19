using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.Repositories.FileUploadsRepository;

public class FileUploadRepository(ApplicationDbContext context) : RepositoryQueryHandler<FileUploadsModel>(context), IFileUploadRepository
{
    //private readonly ApplicationDbContext _context = context;

    // public async Task<FileUploadsModel?>
    //     GetByNameAsync(string fileKey, CancellationToken cancellationToken = default) =>
    //     await _context.FileUploadsModel
    //         .FirstOrDefaultAsync(r => r.S3Key.ToLower() == fileKey.ToLower(), cancellationToken);

    // public new async Task<FileUploadsModel> CreateAsync(FileUploadsModel entity)
    // {
    //     _context.FileUploadsModel.Add(entity);
    //     await _context.SaveChangesAsync();
    //     return entity;
    // }
}