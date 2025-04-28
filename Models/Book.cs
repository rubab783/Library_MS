using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Book
{
    public int Bookid { get; set; }

    public string Title { get; set; } = null!;

    public int Authorid { get; set; }

    public string? Genre { get; set; }

    public int? Publishedyear { get; set; }

    public string? Isbn { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
