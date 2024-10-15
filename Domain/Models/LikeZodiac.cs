using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class LikeZodiac
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ZodiacLikeId { get; set; }
}
