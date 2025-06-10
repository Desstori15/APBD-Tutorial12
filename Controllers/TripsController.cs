using Microsoft.AspNetCore.Mvc;
using APBD_Tutorial12.Repositories;
using APBD_Tutorial12.DTOs;

namespace APBD_Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripRepository _repository;

    public TripsController(ITripRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _repository.GetTripsAsync();
        return Ok(trips);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripDTO dto)
    {
        if (idTrip != dto.IdTrip)
            return BadRequest("Trip ID mismatch.");

        var tripExists = await _repository.DoesTripExistAsync(dto.IdTrip);
        if (!tripExists)
            return NotFound("Trip not found.");

        var clientExists = await _repository.DoesClientExistAsync(dto.Pesel);
        if (!clientExists)
            return NotFound("Client not found.");

        var isAssigned = await _repository.IsClientAlreadyAssignedAsync(dto.Pesel, dto.IdTrip);
        if (isAssigned)
            return Conflict("Client already assigned to trip.");

        var result = await _repository.AssignClientToTripAsync(dto);
        return Ok($"Client assigned with ID {result}");
    }
}