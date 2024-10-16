namespace Infrastructure.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? TelephoneNumber { get; set; }

    public byte? Status { get; set; }

    public string? RoleName { get; set; }

    public string? ConfirmationToken { get; set; }

    public bool IsConfirmed { get; set; }

    public int ZodiacId { get; set; }

    public string Decription { get; set; } = null!;

    public int LilkeListId { get; set; }
}
