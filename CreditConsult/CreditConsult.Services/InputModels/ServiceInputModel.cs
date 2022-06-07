namespace CreditConsult.Services.InputModels;

using System.ComponentModel.DataAnnotations;

public class ServiceInputModel
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    [Range(typeof(decimal), "0", "10000")]
    public decimal Fee { get; set; }

    [Url]
    public string ImageUrl { get; set; }
}
