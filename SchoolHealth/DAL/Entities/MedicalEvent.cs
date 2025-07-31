using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MedicalEvent
{
    public int EventId { get; set; }

    public int StudentId { get; set; }

    public int? NurseId { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public string? TreatmentGiven { get; set; }

    public virtual UserAccount? Nurse { get; set; }

    public virtual Student Student { get; set; } = null!;

}
