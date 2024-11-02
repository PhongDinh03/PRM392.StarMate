using System;
using System.Collections.Generic;

namespace Domain.Models;

public  class LikeZodiac
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ZodiacLikeId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Zodiac ZodiacLike { get; set; } = null!;
}
