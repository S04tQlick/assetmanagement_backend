using Serilog;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.API.DAL.SanityImageDirectory.Repositories;


namespace AssetManagement.API.DAL.SanityImageDirectory.Services;

public class SanityImageService(ISanityImageRepository repo) : ISanityImageService
{
    public async Task<SanityUploadResponse?> CreateAsync(SanityUploadRequest request)
    {
        var response = await repo.UploadImageAsync(request);
        Log.Information("{Image} Successfully Created", request);
        return response;
    }

    public async Task<SanityUploadResponse?> ReplaceImageAsync(string documentId, string oldAssetId, SanityUploadRequest request)
    {
        // 1. Upload new file
        var newAsset = await repo.UploadImageAsync(request);
        if (newAsset == null) return null;

        // 2. Update document reference to new asset
        var updated = await repo.UpdateImageReferenceAsync(documentId, newAsset.Document.Id);
        if (!updated) return null;

        // 3. Delete old asset if no other references
        var refCount = await repo.GetAssetReferenceCountAsync(oldAssetId);
        if (refCount == 0)
        {
            await repo.DeleteAssetAsync(oldAssetId);
            Log.Information("Deleted old asset {OldAssetId} after replacement", oldAssetId);
        }
        else
        {
            Log.Information("Old asset {OldAssetId} still referenced {Count} times, not deleted", oldAssetId, refCount);
        }

        return newAsset;
    }
}



// public async Task<SanityUploadResponse?> ReplaceImageSafeAsync(
//          string documentId, string oldAssetId, IFormFile newFile)
//      {
//          // 1️⃣ Upload new
//          var upload = await repo.UploadImageAsync(newFile)
//                       ?? throw new Exception("Upload failed");
//          var newAssetId = upload.Document.Id;
//  
//          // 2️⃣ Patch doc ref
//          var updated = await repo.UpdateImageReferenceAsync(documentId, newAssetId);
//          if (!updated) throw new Exception("Failed updating document");
//  
//          // 3️⃣ Check if old asset is used anywhere else
//          var count = await repo.GetAssetReferenceCountAsync(oldAssetId);
//          if (count == 0)
//              await repo.DeleteAssetAsync(oldAssetId);
//  
//          return upload;
//      }