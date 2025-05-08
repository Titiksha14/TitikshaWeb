using System.Reflection.Metadata.Ecma335;
using AutoMapper;
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

        public WalkDifficultiesController(IWalkDifficulty walkDifficultyRepository, IMapper mapper)
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
    }
}

