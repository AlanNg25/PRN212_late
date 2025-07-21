using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime DatePosted { get; set; }

    public int? AuthorId { get; set; }

    public virtual UserAccount? Author { get; set; }
}
