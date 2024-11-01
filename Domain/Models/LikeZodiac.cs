using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class LikeZodiac
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ZodiacLikeId { get; set; }


    public virtual User User { get; set; } = null!;
    public virtual Zodiac Zodiac { get; set; } = null!;
}
