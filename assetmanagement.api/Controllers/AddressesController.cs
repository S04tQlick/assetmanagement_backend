using AssetManagement.API.DAL.Services.AddressesService;
using AssetManagement.Entities.DTOs.Requests;
using AssetManagement.Entities.DTOs.Responses;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController(IAddressesService service) : BaseApiController<AddressesModel, AddressesResponse, AddressesCreateRequest, AddressesUpdateRequest>(service)
{
    // // GET: api/addresses
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<AddressesResponse>>> GetAll()
    // {
    //     var addresses = await service.GetAllAsync();
    //     return Ok(addresses);
    // }
    //
    // // GET: api/addresses/{id}
    // [HttpGet("{id:guid}")]
    // public async Task<ActionResult<AddressesResponse>> GetById(Guid id)
    // {
    //     var address = await service.GetByIdAsync(id);
    //     if (address == null)
    //         return NotFound(new { message = $"Address with id '{id}' not found." });
    //
    //     return Ok(address);
    // }
    //
    // // GET: api/addresses/city/{city}
    // [HttpGet("city/{city}")]
    // public async Task<ActionResult<IEnumerable<AddressesResponse>>> GetByCity(string city)
    // {
    //     var addresses = await service.GetByCityAsync(city);
    //     return Ok(addresses);
    // }
    //
    // // POST: api/addresses
    // [HttpPost]
    // public async Task<ActionResult<AddressesResponse>> Create(AddressesCreateRequest request)
    // {
    //     var created = await service.CreateAsync(request);
    //     return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    // }
    //
    // // PUT: api/addresses/{id}
    // [HttpPut("{id:guid}")]
    // public async Task<ActionResult<AddressesResponse>> Update(Guid id, AddressesUpdateRequest request)
    // {
    //     var updated = await service.UpdateAsync(id, request);
    //     if (updated == null)
    //         return NotFound(new { message = $"Address with id '{id}' not found." });
    //
    //     return NoContent();
    // }
    //
    // // DELETE: api/addresses/{id}
    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> Delete(Guid id)
    // {
    //     var deleted = await service.DeleteAsync(id);
    //     if (deleted == 0)
    //         return NotFound(new { message = $"Address with id '{id}' not found." });
    //
    //     return NoContent();
    // }
}