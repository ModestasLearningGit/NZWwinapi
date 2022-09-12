using Microsoft.EntityFrameworkCore;
using WZWalksAPI.Data;
using WZWalksAPI.Models.Domain;

namespace WZWalksAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nzWalksDBContext;

        public WalkRepository(NZWalksDbContext NZWalksDBContext)
        {
            this._nzWalksDBContext = NZWalksDBContext;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _nzWalksDBContext.Walks
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await _nzWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
           //assing new id
            walk.Id = Guid.NewGuid();
            await _nzWalksDBContext.Walks.AddAsync(walk);
            await _nzWalksDBContext.SaveChangesAsync();

            return walk;


        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await _nzWalksDBContext.Walks.FindAsync(id);
            if(existingWalk != null)
            {
                existingWalk.Leght = walk.Leght;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await _nzWalksDBContext.SaveChangesAsync();

                return existingWalk;
            }
            return null;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await _nzWalksDBContext.Walks.FindAsync(id);

            if(existingWalk == null)
            {
                return null;
            }
            _nzWalksDBContext.Walks.Remove(existingWalk);
            await _nzWalksDBContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
