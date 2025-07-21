using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MedicineSent
{
    public int SendId { get; set; }

    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public string MedicineName { get; set; } = null!;

    public string? Dosage { get; set; }

    public string? Instruction { get; set; }

    public virtual Parent Parent { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
