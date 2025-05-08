using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;

namespace WebAPITitiksha.API.Repository
{
    public class WalkDifficultyRepository:IWalkDifficulty
    {
        private readonly WebTitikshaDbContext webTitikshaDbContext;

        public WalkDifficultyRepository(WebTitikshaDbContext webTitikshaDbContext)
        {
            this.webTitikshaDbContext = webTitikshaDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id=Guid.NewGuid();
            await webTitikshaDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            await webTitikshaDbContext.SaveChangesAsync();
            return (walkDifficulty);
        }

        public async Task<WalkDifficulty> AddGetIDAsync(Guid id)
        {
           return await webTitikshaDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWD = await webTitikshaDbContext.WalkDifficulties.FindAsync(id);
            if (existingWD == null)
            {
                return null;
            }
            webTitikshaDbContext.Remove(existingWD);
            await webTitikshaDbContext.SaveChangesAsync();
            return existingWD;
            
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await webTitikshaDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWD = await webTitikshaDbContext.WalkDifficulties.FindAsync(id);
            if(existingWD == null)
            {
                return null;

            }
            existingWD.Code=walkDifficulty.Code;
            await webTitikshaDbContext.SaveChangesAsync();
            return (existingWD);

        }
    }
}
