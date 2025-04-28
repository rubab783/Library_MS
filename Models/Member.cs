using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Member
{
    public int Memberid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? Joindate { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
