namespace CreditConsult.Services.InputModels;

using System.ComponentModel.DataAnnotations;

public class AppointmentInputModel
{
    [Required]
    [MaxLength(50)]
    public string Employee { get; set; }

    [Required]
    [MaxLength(30)]
    public string Date { get; set; }

    [Required]
    [MaxLength(30)]
    public string Time { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClientName { get; set; }

    [Required]
    [MaxLength(15)]
    [Phone]
    public string ClientPhone { get; set; }

    [Required]
    [EmailAddress]
    public string ClientEmail { get; set; }

    public string? Message { get; set; }

    [Required]
    [MaxLength(100)]
    public string ServiceName { get; set; }
}
