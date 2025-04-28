using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class BorrowRecord
{
    public int Recordid { get; set; }

    public int Memberid { get; set; }

    public int Bookid { get; set; }

    public DateOnly Borrowdate { get; set; }

    public DateOnly? Returndate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
