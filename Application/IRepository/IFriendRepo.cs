using Domain.Models;

namespace Application.IRepository;
public interface IFriendRepo : IGenericRepo<Friend>
{
    Task<List<Friend>> GetListF();
    Task<Friend?> GetFById(int userId);
    Task<List<Friend?>> GetFByUserId(int userId);

    Task<Friend?> GetFriendshipByUserAndFriendId(int userId, int friendId);

    Task<bool> UpdateFriendshipStatus(int userId, int friendId, bool status);
    //Task<Booking?> GetBookingByUser(string userId);

}
