using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;
using WebAPITitiksha.API.Models.DTO;
using WebAPITitiksha.API.Repository;

namespace WebAPITitiksha.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;


        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[Controller]")]

        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database-domain walks
            var WalksDomain = await walkRepository.GetAllAsync();
            //Convert domain walks to DTO walks
            var WalksDTo = mapper.Map<List<Models.DTO.WalkDto>>(WalksDomain);
            //return response
            return Ok(WalksDTo);

        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetwalkAsync")]
        public async Task<IActionResult> GetwalkAsync(Guid id)
        {
            //Get Walk Domain object from database
            var walkDomain = await walkRepository.GetAsync(id);

            //convert Domain object to DTO
            var WalkDTO = mapper.Map<Models.DTO.WalkDto>(walkDomain);

            //Return response 
            return Ok(WalkDTO);
        }


        //}
        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddwalkRequest addWalkRequest)
        {


            // Convert DTO to Domain Object
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            // Pass domain object to Repository to persist this
            walkDomain = await walkRepository.AddAsync(walkDomain);

            // Convert the Domain object back to DTO
            var walkDTO = new Models.DTO.WalkDto
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            // Send DTO response back to Client
            return CreatedAtAction(nameof(GetwalkAsync), new { id = walkDTO.Id }, walkDTO);
        }
       
        [HttpPut]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
                [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {

            // Convert DTO to Domain object
            var walkDomain = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // Pass details to Repository - Get Domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            // Handle Null (not found)
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert back Domain to DTO
            var walkDTO = new Models.DTO.WalkDto
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            // Return Response
            return Ok(walkDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // call Repository to delete walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.WalkDto>(walkDomain);

            return Ok(walkDTO);
        }


    }

}
