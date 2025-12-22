using System;
using System.Collections.Generic;

namespace Database.Shared.Models;

public partial class TblProduct
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public bool? IsDelete { get; set; }
}
