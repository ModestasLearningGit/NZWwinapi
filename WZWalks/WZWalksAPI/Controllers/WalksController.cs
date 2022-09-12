using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WZWalksAPI.Models.DTO;
using WZWalksAPI.Repositories;

namespace WZWalksAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this._walkRepository = walkRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data  from db - domain walks
            var walksDomain = await _walkRepository.GetAllAsync();
            //convert data to DTO object
            var walksDTO = _mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            //return response
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //fetch data  from db - domain walks
            var walkDomain = await _walkRepository.GetAsync(id);
            //convert data to DTO object
            var walksDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);
            //return response
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //convert dto to domain object
            var walkDomain = new Models.Domain.Walk
            {
                Leght = addWalkRequest.Leght,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            //pass domain object to repository
            walkDomain = await _walkRepository.AddAsync(walkDomain);
            //conver domain model to dto
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Leght = walkDomain.Leght,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            //send dto response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
                [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //conver DTO to domain object
            var walkDomain = new Models.Domain.Walk
            {

                Leght = updateWalkRequest.Leght,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            //pass detais to repository
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);
            //handle null
            if (walkDomain == null)
            {
                return NotFound();
            }

            //Convert back domain to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Leght = walkDomain.Leght,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //return response
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //call repository to delete walk
            var walkDomain = await _walkRepository.DeleteAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }

            //var walksDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);

            var walksDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Leght = walkDomain.Leght,
                Name = walkDomain.Name,
                RegionId  = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            return Ok(walksDTO);
        }
    }
}
