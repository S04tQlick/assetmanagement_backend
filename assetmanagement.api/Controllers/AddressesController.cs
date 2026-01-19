using AssetManagement.API.DAL.Services.AddressesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController(IAddressService service)
    : BaseApiController<AddressesModel, AddressesResponse, AddressesCreateRequest, AddressesUpdateRequest>(service)
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<AddressesResponse>>> GetByInstitutionId(Guid institutionId) =>
        Task.FromResult<ActionResult<IEnumerable<AddressesResponse>>>(NotFound());
}


//
    // // GET: api/addresses/city/{city}
    // [HttpGet("city/{city}")]
    // public async Task<ActionResult<IEnumerable<AddressesResponse>>> GetByCity(string city)
    // {
    //     var addresses = await service.GetByCityAsync(city);
    //     return Ok(addresses);
    // }
//}