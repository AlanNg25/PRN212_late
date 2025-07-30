using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class UserAccount
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<MedicalEvent> MedicalEvents { get; set; } = new List<MedicalEvent>();

    public virtual Parent? Parent { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
