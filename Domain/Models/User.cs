namespace Infrastructure.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!; // Update this if TelephoneNumber is not nullable

    public byte Status { get; set; }

    public string? RoleName { get; set; }

    public string? ConfirmationToken { get; set; }

    public bool IsConfirmed { get; set; }

    public int? ZodiacId { get; set; }

    public string Description { get; set; } = null!; // Corrected spelling

    public int? LikeListId { get; set; }  // Corrected spelling
}
