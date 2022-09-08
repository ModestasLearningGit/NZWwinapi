using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WZWalksAPI.Models.Domain;
using WZWalksAPI.Repositories;

namespace WZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionReposatary _regionReposatary;
        private readonly IMapper _mapper;

        public RegionsController(IRegionReposatary regionReposatary, IMapper mapper)
        {
            this._regionReposatary = regionReposatary;
            this._mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionReposatary.GetAllAsync();

            //return DTO Regions nwithout automapper
            /* var regionsDTO = new List<Models.DTO.Region>();
             regions.ToList().ForEach(region =>
             {
                 var regionDTO = new Models.DTO.Region()
                 {
                     Id = region.Id,
                     Code = region.Code,
                     Name = region.Name,
                     Area = region.Area,
                     Lat =  region.Lat,
                     Long = region.Long,
                     Population = region.Population
                 };

                 regionsDTO.Add(regionDTO);
             });*/

            var regionsDTO =_mapper.Map <List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

    }
}
