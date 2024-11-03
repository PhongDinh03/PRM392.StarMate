using Domain.Models;

namespace Application.IRepository
{
    public interface ILikeZodiacRepo : IGenericRepo<LikeZodiac>
    {
        Task<List<LikeZodiac>> GetListLikeZodiacAsync();
        Task<LikeZodiac?> GetLikeZodiacByIdAsync(int id);
    }
}
