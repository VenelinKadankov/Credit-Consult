namespace CreditConsult.Data.Common.Interfaces;

public interface IBaseModel
{
    DateTime CreatedOn { get; set; }

    DateTime? ModifiedOn { get; set; }

    bool IsDeleted { get; set; }

    DateTime? DeletedOn { get; set; }
}
