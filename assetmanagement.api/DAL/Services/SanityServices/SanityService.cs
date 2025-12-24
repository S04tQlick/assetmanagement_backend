using AssetManagement.API.Constants;
using AssetManagement.API.DAL.Repositories.SanityRepository;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Enums;
using AssetManagement.Entities.GeneralResponse;
using Serilog;

namespace AssetManagement.API.DAL.Services.SanityServices;

public class SanityService(ISanityRepository sanityRepository) : ISanityService
{
    public async Task<BaseActionResponse<object>> CreateAsync(SanityUploadRequest request)
          {
              var response = await sanityRepository.CreateSanityUploadAsync(request);
              Log.Information("{Image} Successfully Created", request);
              return new BaseActionResponse<object>
              {
                  Success = true,
                  Message = MessageConstants.Success(RecordTypeEnum.Save),
                  Data = response
              };
          }
      
          public async Task<BaseActionResponse<object>> UpdateAsync(string imageId, SanityUploadRequest request)
          {
              var response = await sanityRepository.UpdateSanityUploadAsync(imageId,request);  
              Log.Information("{Image} Successfully Updated", request);
              return new BaseActionResponse<object>
              {
                  Success = true,
                  Message = MessageConstants.Success(RecordTypeEnum.Save),
                  Data = response
              };
          }
}









