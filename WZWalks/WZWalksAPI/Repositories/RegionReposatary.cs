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
    }
}
