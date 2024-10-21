using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public partial class User
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
    public string TelephoneNumber { get; set; } = null!;

    public byte Status { get; set; }

    public string? RoleName { get; set; }

    public string? ConfirmationToken { get; set; }

    public bool IsConfirmed { get; set; }

    public int? ZodiacId { get; set; }

    public string? Description { get; set; } 

    public int? LikeListId { get; set; } 
}
