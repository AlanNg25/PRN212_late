using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class HealthRecord
{
    public int RecordId { get; set; }

    public int StudentId { get; set; }

    public string? Allergy { get; set; }

    public string? ChronicDisease { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Vision { get; set; }

    public string? Hearing { get; set; }

    public virtual Student Student { get; set; } = null!;
}
