namespace Infrastructure.Models;

public partial class Friend
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FriendId { get; set; }

    public bool? status { get; set; }
    public virtual User FriendUser { get; set; } = null!;

}
