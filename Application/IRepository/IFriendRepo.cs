
using Infrastructure.Models;

namespace Application.IRepository;
public interface IFriendRepo : IGenericRepo<Friend>
{
    Task<List<Friend>> GetListF();
    Task<Friend?> GetFById(int userId);
    Task<List<Friend?>> GetFByUserId(int userId);

    Task<Friend?> GetFriendshipByUserAndFriendId(int userId, int friendId);
    //Task<Booking?> GetBookingByUser(string userId);

}
