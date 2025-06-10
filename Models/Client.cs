using System.ComponentModel.DataAnnotations;

namespace APBD_Tutorial12.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }

    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; } = null!;

    [MaxLength(120)]
    public string Email { get; set; }

    [MaxLength(60)]
    public string Telephone { get; set; }

    [MaxLength(11)]
    public string Pesel { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}