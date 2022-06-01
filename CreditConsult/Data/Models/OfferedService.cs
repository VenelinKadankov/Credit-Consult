namespace CreditConsult.Data.Models;

using System.ComponentModel.DataAnnotations;
using CreditConsult.Data.Common;

public class OfferedService : BaseModel<int>
{
    [Required]
    [MaxLength(50)]
    public string? Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [Range(typeof(decimal), "0", "10000")]
    public decimal? Fee { get; set; }

    [Url]
    public string? ImageUrl { get; set; }

    public ICollection<ApplicationUser> Employees { get; set; } = new HashSet<ApplicationUser>();
}
