using Application.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LikeZodiacRepo : GenericRepo<LikeZodiac>, ILikeZodiacRepo
    {
        private readonly ZodiacTinderContext _context;
        public LikeZodiacRepo(ZodiacTinderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LikeZodiac?> GetLikeZodiacByIdAsync(int id)
        {
            return await _context.LikeZodiacs.Include(l => l.User).Include(l => l.ZodiacLike).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<LikeZodiac>> GetListLikeZodiacAsync()
        {
            return await _context.LikeZodiacs.Include(l => l.User).Include(l => l.ZodiacLike).ToListAsync();
        }
    }
}
