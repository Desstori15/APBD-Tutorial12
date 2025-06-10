using APBD_Tutorial12.DTOs;

namespace APBD_Tutorial12.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<TripDTO>> GetTripsAsync();
    Task<bool> DoesClientExistAsync(string pesel);
    Task<bool> IsClientAlreadyAssignedAsync(string pesel, int idTrip);
    Task<bool> DoesTripExistAsync(int idTrip);
    Task<int> AssignClientToTripAsync(AssignClientToTripDTO dto);
}