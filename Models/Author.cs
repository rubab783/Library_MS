using System;
using System.Collections.Generic;

namespace LMS.Models;

public partial class Author
{
    public int Authorid { get; set; }

    public string Authorname { get; set; } = null!;

    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
