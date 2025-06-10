using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Tutorial12.Models;

public class ClientTrip
{
    [Key, Column(Order = 0)]
    public int IdClient { get; set; }

    [Key, Column(Order = 1)]
    public int IdTrip { get; set; }

    [Required]
    public DateTime RegisteredAt { get; set; }

    public string? PaymentDate { get; set; }

    public virtual Client Client { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
}
