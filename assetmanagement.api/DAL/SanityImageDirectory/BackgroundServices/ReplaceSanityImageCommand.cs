using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using MediatR;

namespace AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;

public abstract record ReplaceSanityImageCommand(
    string DocumentId,
    string OldAssetId,
    SanityUploadRequest NewImage) : IRequest<SanityUploadResponse>;