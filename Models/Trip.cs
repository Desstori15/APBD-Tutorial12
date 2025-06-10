using System.ComponentModel.DataAnnotations;

namespace APBD_Tutorial12.Models;

public class Trip
{
    [Key]
    public int IdTrip { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = null!;

    [MaxLength(220)]
    public string? Description { get; set; }

    [Required]
    public DateTime DateFrom { get; set; }

    [Required]
    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();

    
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}