using Microsoft.EntityFrameworkCore;
using WZWalksAPI.Data;
using WZWalksAPI.Models.Domain;
using WZWalksAPI.Models.DTO;

namespace WZWalksAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nzWalksDBContext;

        public WalkDifficultyRepository(NZWalksDbContext NZWalksDBContext)
        {
            this._nzWalksDBContext = NZWalksDBContext;
        }

        public async Task<IEnumerable<Models.Domain.WalkDifficulty>> GetAllAsync()
        {
            return await _nzWalksDBContext.WalkDifficulty.ToListAsync();
        }

        public async Task<Models.Domain.WalkDifficulty> GetAsync(Guid id)
        {
            var walkDificulty = await _nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            return walkDificulty;
        }

        public async Task<Models.Domain.WalkDifficulty> AddAsync(Models.Domain.WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _nzWalksDBContext.WalkDifficulty.AddAsync(walkDifficulty);
            await _nzWalksDBContext.SaveChangesAsync();

            return walkDifficulty;
        }
        public async Task<Models.Domain.WalkDifficulty> UpdateAsync(Guid id, Models.Domain.WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await _nzWalksDBContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }
        public async Task<Models.Domain.WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            _nzWalksDBContext.WalkDifficulty.Remove(walkDifficulty);
            await _nzWalksDBContext.SaveChangesAsync();

            return walkDifficulty;
        }


        
    }
}
