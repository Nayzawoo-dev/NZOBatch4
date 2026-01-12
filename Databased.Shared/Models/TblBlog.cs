using System;
using System.Collections.Generic;

namespace Databased.Blog.Models;

public partial class TblBlog
{
    public int Id { get; set; }

    public string? Caption { get; set; }

    public DateTime Date { get; set; }
}
