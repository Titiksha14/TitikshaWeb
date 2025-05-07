using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;
using WebAPITitiksha.API.Models.DTO;

namespace WebAPITitiksha.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WebTitikshaDbContext webTitikshaDbContext;

        public WalkRepository(WebTitikshaDbContext webTitikshaDbContext)
        {
            this.webTitikshaDbContext = webTitikshaDbContext;
        }

        

        public async Task<Walk> AddAsync(Walk walk)
        {
            // Assign New ID
            walk.Id = Guid.NewGuid();
            await webTitikshaDbContext.Walks.AddAsync(walk);
            await webTitikshaDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk =await  webTitikshaDbContext.Walks.FindAsync(id);
            if (walk == null)
            {
                return null;
            }
            webTitikshaDbContext.Walks.Remove(walk);
            webTitikshaDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
             return await 
                webTitikshaDbContext.Walks
               // .Include(x => x.Region)
                //.Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await webTitikshaDbContext.Walks
                //.Include(x =>x.Region)
                //.Include(x =>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
           

        }

        public async Task<Walk> UpdateAsync(Guid id,Walk walk)
        {

            var existingWalk = await webTitikshaDbContext.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;

                await webTitikshaDbContext.SaveChangesAsync();
                return existingWalk;
            }

            return null;


        }

        
    }
}
