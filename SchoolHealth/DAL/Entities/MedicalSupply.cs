using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MedicalSupply
{
    public int SupplyId { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public DateOnly? ExpirationDate { get; set; }
}
