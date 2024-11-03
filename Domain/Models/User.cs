using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public  class User
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string? Gender { get; set; }
    [Required]
    public string TelephoneNumber { get; set; } = null!;

    public byte Status { get; set; }

    public string? RoleName { get; set; }

    public string? ConfirmationToken { get; set; }

    public bool IsConfirmed { get; set; }
    [Required]
    public int? ZodiacId { get; set; }

    public string? Description { get; set; }

    public int? LikeListId { get; set; }

    public virtual ICollection<Friend> FriendFriendNavigations { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUsers { get; set; } = new List<Friend>();

    public virtual ICollection<LikeZodiac> LikeZodiacs { get; set; } = new List<LikeZodiac>();

    public virtual Zodiac? Zodiac { get; set; }
}
