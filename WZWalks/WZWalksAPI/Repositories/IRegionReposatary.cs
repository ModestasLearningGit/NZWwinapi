using WZWalksAPI.Models.Domain;

namespace WZWalksAPI.Repositories
{
    public interface IRegionReposatary
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
