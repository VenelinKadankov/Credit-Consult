namespace CreditConsult.Data.Models;

using System.ComponentModel.DataAnnotations;
using CreditConsult.Data.Common;

public class AppointmentsForDay : BaseModel<int>
{
    [Required]
    public string? ApplicationUserId { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Date { get; set; }

    public IEnumerable<HourForAppontment> AppointmentsHours { get; set; } = new HashSet<HourForAppontment>();
}
