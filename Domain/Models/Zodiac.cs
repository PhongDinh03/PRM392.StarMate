using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Zodiac
{
    public int Id { get; set; }

    public string NameZodiac { get; set; } = null!;

    public string DesZodiac { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<LikeZodiac> LikedByUsers { get; set; } = new List<LikeZodiac>();
}
