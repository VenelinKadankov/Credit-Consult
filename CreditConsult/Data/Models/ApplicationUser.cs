namespace CreditConsult.Data.Models;

using Microsoft.AspNetCore.Identity;
using CreditConsult.Data.Common.Interfaces;
using CreditConsult.Data.Models.Enums;

public class ApplicationUser : IdentityUser, IBaseModel
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Roles = new HashSet<IdentityUserRole<string>>();
        this.Claims = new HashSet<IdentityUserClaim<string>>();
        this.Logins = new HashSet<IdentityUserLogin<string>>();
        this.Appointments = new HashSet<Appointment>();
        this.DailyAppointments = new HashSet<AppointmentsForDay>();
        this.OfferedServices = new HashSet<OfferedService>();
    }

    public bool IsEmployee { get; set; }

    public UserTitle? Title { get; set; }

    // Audit info
    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    // Deletable entity
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<Appointment> Appointments { get; init; }

    public virtual ICollection<AppointmentsForDay> DailyAppointments { get; set; }

    public virtual ICollection<OfferedService> OfferedServices { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Roles { get; init; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; init; }

    public virtual ICollection<IdentityUserLogin<string>> Logins { get; init; }
}
