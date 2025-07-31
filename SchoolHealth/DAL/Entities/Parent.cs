using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Parent
{
    public int ParentId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>(); 

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<HealthCheckForm> HealthCheckForms { get; set; } = new List<HealthCheckForm>();

    public virtual ICollection<MedicineSent> MedicineSents { get; set; } = new List<MedicineSent>();

    public virtual ICollection<VaccinationConsentForm> VaccinationConsentForms { get; set; } = new List<VaccinationConsentForm>();
}

