using APBD_Tutorial12.DTOs;
using APBD_Tutorial12.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Tutorial12.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    } 
    public async Task<IEnumerable<TripDTO>> GetTripsAsync()
    {
        return await _context.Trips
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.Client)
            .Include(t => t.Countries)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList(),
                Countries = t.Countries.Select(c => c.Name).ToList()
            })
            .ToListAsync();
    }





    public async Task<bool> DoesClientExistAsync(string pesel)
    {
        return await _context.Clients.AnyAsync(c => c.Pesel == pesel);
    }

    public async Task<bool> IsClientAlreadyAssignedAsync(string pesel, int idTrip)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
        if (client == null) return false;

        return await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);
    }

    public async Task<bool> DoesTripExistAsync(int idTrip)
    {
        return await _context.Trips.AnyAsync(t => t.IdTrip == idTrip);
    }

    public async Task<int> AssignClientToTripAsync(AssignClientToTripDTO dto)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);

        if (client == null)
        {
            client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = dto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        };

        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();

        return clientTrip.IdClient;
    }
}
