namespace CreditConsult.Data.Models;

using System.ComponentModel.DataAnnotations;
using CreditConsult.Data.Common;

public class HourForAppontment : BaseModel<int>
{
    //public int DailyAppointmentsId { get; init; }

    public AppointmentsForDay? DailyAppointments { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Time { get; set; }
}
