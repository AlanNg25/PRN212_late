using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class VaccinationConsentForm
{
    public int FormId { get; set; }

    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public DateTime SentDate { get; set; }

    public bool? Confirmed { get; set; }

    public virtual Parent Parent { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
