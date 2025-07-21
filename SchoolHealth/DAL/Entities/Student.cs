using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? Class { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<HealthCheckForm> HealthCheckForms { get; set; } = new List<HealthCheckForm>();

    public virtual ICollection<HealthCheck> HealthChecks { get; set; } = new List<HealthCheck>();

    public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();

    public virtual ICollection<MedicalEvent> MedicalEvents { get; set; } = new List<MedicalEvent>();

    public virtual ICollection<MedicineSent> MedicineSents { get; set; } = new List<MedicineSent>();

    public virtual Parent? Parent { get; set; }

    public virtual ICollection<VaccinationConsentForm> VaccinationConsentForms { get; set; } = new List<VaccinationConsentForm>();

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
