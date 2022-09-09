using Microsoft.EntityFrameworkCore;
using WZWalksAPI.Data;
using WZWalksAPI.Models.Domain;

namespace WZWalksAPI.Repositories
{
    public class RegionReposatary : IRegionReposatary
    {
        private readonly NZWalksDbContext _nzWalksDBContext;

        public RegionReposatary(NZWalksDbContext NZWalksDBContext)
        {
            _nzWalksDBContext = NZWalksDBContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nzWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await _nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;
        }
        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nzWalksDBContext.Regions.AddAsync(region);
            await _nzWalksDBContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null)
            {
                return null;
            }

            _nzWalksDBContext.Regions.Remove(region);
            await _nzWalksDBContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateAsync(Guid id,Region region)
        {
            var existingRegion = await _nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await _nzWalksDBContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
