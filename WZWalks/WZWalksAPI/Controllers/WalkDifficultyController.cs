using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WZWalksAPI.Models.DTO;
using WZWalksAPI.Repositories;

namespace WZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this._walkDifficultyRepository = walkDifficultyRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksDifficultiesAsync()
        {
            //fetch data  from db 
            var walksDifficultyDomain = await _walkDifficultyRepository.GetAllAsync();
            //convert data to DTO object
            var walksDifficultyDTO = _mapper.Map<List<Models.DTO.WalkDifficulty>>(walksDifficultyDomain);
            //return response
            return Ok(walksDifficultyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            //fetch data  from db - domain walksDifficulty
            var walkDifficultyDomain = await _walkDifficultyRepository.GetAsync(id);
            
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            //convert data to DTO object
            var walksDifficultyyDTO = _mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            //return response
            return Ok(walksDifficultyyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkRequest)
        {
            //validate request
            if(!ValidateAddWalkAsync(addWalkRequest))
            {
                return BadRequest(ModelState);
            }

            //convert dto to domain object
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
               Code = addWalkRequest.Code
            };

            //pass domain object to repository
            walkDifficultyDomain = await _walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            //conver domain model to dto
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
            };
            //send dto response back to client
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDomain);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
                [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //validate request
            if(!ValidateUpdateWalkAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            //conver DTO to domain object
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            //pass details to repository
            walkDifficultyDomain = await _walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            //handle null
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            //Convert back domain to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
                
            };

            //return response
            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            //call repository to delete walk
            var walkDifficultyDomain = await _walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            //var walksDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);

            var walksDifficultyDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDifficultyDomain.Id,
                Code=walkDifficultyDomain.Code
            };

            return Ok(walksDifficultyDTO);
        }

        #region Private methods
        private bool ValidateAddWalkAsync(Models.DTO.AddWalkDifficultyRequest addWalkRequest)
        {
            if(addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    "Add walk difficulty cannont be null or empty");

                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Code),
                    $"{nameof(addWalkRequest.Code)} cannot be null or white space");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private bool ValidateUpdateWalkAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    "Add walk difficulty cannont be null or empty");

                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null or white space");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
