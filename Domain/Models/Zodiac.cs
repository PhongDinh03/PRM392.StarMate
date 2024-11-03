using System;
using System.Collections.Generic;

namespace Domain.Models;

public  class Zodiac
{
    public int Id { get; set; }

    public string NameZodiac { get; set; } = null!;

    public string DesZodiac { get; set; } = null!;

    public virtual ICollection<LikeZodiac> LikeZodiacs { get; set; } = new List<LikeZodiac>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
