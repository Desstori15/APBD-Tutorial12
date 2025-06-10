using System.ComponentModel.DataAnnotations;

namespace APBD_Tutorial12.Models;

public class Country
{
    [Key]
    public int IdCountry { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();


}