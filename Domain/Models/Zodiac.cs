using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Zodiac
{
    public int Id { get; set; }

    public string NameZodiac { get; set; } = null!;

    public string DesZodiac { get; set; } = null!;
}
