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

    public int Type { get; set; } // 1: Chia sẻ kiến thức y tế, 2:  mẹo vặt

    public virtual UserAccount? Author { get; set; }
}
