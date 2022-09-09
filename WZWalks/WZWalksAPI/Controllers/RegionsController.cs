using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WZWalksAPI.Models.Domain;
using WZWalksAPI.Models.DTO;
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
        public async Task<IActionResult> GetAllRegionsAsync()
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetReagionAsync")]
        public async Task<IActionResult> GetReagionAsync(Guid id)
        {
            var region = await _regionReposatary.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO =_mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            region = await _regionReposatary.AddAsync(region);

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetReagionAsync), new { id = regionDTO.Id},  regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public  async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            region = await _regionReposatary.UpdateAsync(id, region);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await _regionReposatary.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return Ok(regionDTO);
        }
    }
}
