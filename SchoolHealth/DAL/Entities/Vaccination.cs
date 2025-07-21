using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public int StudentId { get; set; }

    public string VaccineName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int? NurseId { get; set; }

    public string? Result { get; set; }

    public virtual UserAccount? Nurse { get; set; }

    public virtual Student Student { get; set; } = null!;
}
