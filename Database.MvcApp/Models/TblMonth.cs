using System;
using System.Collections.Generic;

namespace Database.MvcApp.Models;

public partial class TblMonth
{
    public int Id { get; set; }

    public string? MonthMm { get; set; }

    public string? MonthEn { get; set; }

    public string? FestivalMm { get; set; }

    public string? FestivalEn { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }
}
