namespace CreditConsult.Data.Models;

using System.ComponentModel.DataAnnotations;
using CreditConsult.Data.Common;

public class Appointment : BaseModel<int>
{
    [Required]
    public string? EmployeeId { get; set; }

    public ApplicationUser? Employee { get; set; }

    [Required]
    public string? Date { get; set; }

    [Required]
    public string? Time { get; set; }

    public string? Message { get; set; }

    [Required]
    [MaxLength(50)]
    public string? ClientName { get; set; }

    [Required]
    [Phone]
    [MaxLength(15)]
    public string? ClientPhone { get; set; }

    [Required]
    [EmailAddress]
    public string? ClientEmail { get; set; }

    [Required]
    [MaxLength(50)]
    public string? ServiceName { get; set; }
}
