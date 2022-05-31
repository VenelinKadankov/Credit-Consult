namespace CreditConsult.Data.Common;

using System.ComponentModel.DataAnnotations;
using CreditConsult.Data.Common.Interfaces;

public abstract class BaseModel<TKey> : IBaseModel
{
    [Key]
    public TKey? Id { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

}
