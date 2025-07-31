using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class HealthCheck
{
    public int CheckId { get; set; }

    public int StudentId { get; set; }

    public DateOnly Date { get; set; }

    public string? Result { get; set; }

    public string? DoctorNotes { get; set; }

    public virtual Student Student { get; set; } = null!;

}
