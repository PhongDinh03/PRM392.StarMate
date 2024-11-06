using Domain.Models;

namespace Application.IRepository
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckEmailAddressExisted(string emailaddress);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByConfirmationToken(string token);
        Task<List<User>> GetRandomUsersByZodiacAndGenderAsync(int[] zodiacIds, string gender, int userId);
        Task<User?> GetUserById(int id);
    }
}
