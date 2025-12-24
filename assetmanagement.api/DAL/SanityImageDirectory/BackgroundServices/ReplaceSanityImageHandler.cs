using AssetManagement.API.DAL.SanityImageDirectory.Services;
using AssetManagement.Entities.DTOs.Responses;
using MediatR;

namespace AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;

// public class ReplaceSanityImageHandler(SanityImageService service)
//     : IRequestHandler<ReplaceSanityImageCommand, SanityUploadResponse>
// {
//     public async Task<SanityUploadResponse> Handle(ReplaceSanityImageCommand cmd, CancellationToken ct)
//     {
//         var result = await service.ReplaceImageSafeAsync(cmd.DocumentId, cmd.OldAssetId, cmd.NewImage)
//                      ?? throw new InvalidOperationException("Upload or update operation returned null.");
//
//         return result;
//     }
// }



public class ReplaceSanityImageHandler(ISanityImageService service)
    : IRequestHandler<ReplaceSanityImageCommand, SanityUploadResponse>
{
    public async Task<SanityUploadResponse> Handle(
        ReplaceSanityImageCommand cmd, 
        CancellationToken ct)
    {
        var result = await service.ReplaceImageAsync(
            cmd.DocumentId, 
            cmd.OldAssetId, 
            cmd.NewImage
        ) ?? throw new InvalidOperationException("Upload or update operation returned null.");

        return result;
    }
}
