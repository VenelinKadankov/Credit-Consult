namespace CreditConsult.Data.Models;

using Microsoft.AspNetCore.Identity;
using CreditConsult.Data.Common.Interfaces;

public class ApplicationRole : IdentityRole, IBaseModel
{
    public ApplicationRole()
       : this(null)
    {
    }

    public ApplicationRole(string name)
        : base(name)
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}
