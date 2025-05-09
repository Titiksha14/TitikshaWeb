using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;
using WebAPITitiksha.API.Models.DTO;
using WebAPITitiksha.API.Repository;

namespace WebAPITitiksha.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficulty walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficulty walkDifficultyRepository, IMapper mapper,IWalkDifficulty WalkDiffWalkDifficultyRepository)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]

        public async Task<ActionResult> GetAllWDAsync()
        {
            //fetch data from database-domain walks
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();
            //Convert domain walks to DTO walks
            var walkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficultyDto>>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWDMethodID")]
        public async Task<IActionResult> GetWDMethodID(Guid id)
        {
            var walkDiffcultyDomain = await walkDifficultyRepository.AddGetIDAsync(id);
            var WalkDiffcultyDTO = mapper.Map<Models.DTO.WalkDifficultyDto>(walkDiffcultyDomain);

            return Ok(WalkDiffcultyDTO);


        }
        [HttpPost]
        public async Task<IActionResult> AddWDAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //validate 
            if (! await ValidateAddWDAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            var walkDiffcultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            walkDiffcultyDomain = await walkDifficultyRepository.AddAsync(walkDiffcultyDomain);

            var WalkDiffcultyDTO = mapper.Map<Models.DTO.WalkDifficultyDto>(walkDiffcultyDomain);

            return CreatedAtAction(nameof(GetWDMethodID), new { id = WalkDiffcultyDTO.Id }, WalkDiffcultyDTO);

        }
        [HttpPut]
        [Route("{Id:guid}")]

        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            
                //validate 
            if (!await ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if (walkDifficultyDomain == null)
            {

                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficultyDto>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteWDASync(Guid id)
        {
            var walkDifficultyDomain =await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            { 
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficultyDto>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);

        }
        #region Private methods 
        private async Task<bool> ValidateAddWDAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if(addWalkDifficultyRequest == null)
            {

                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)}cannot be empty.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateUpdateWalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {

                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"{nameof(updateWalkDifficultyRequest)}cannot be empty.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
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

