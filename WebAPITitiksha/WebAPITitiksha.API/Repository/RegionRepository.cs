using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebAPITitiksha.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WebTitikshaDbContext webTitikshaDbContext;

        public RegionRepository(WebTitikshaDbContext webTitikshaDbContext)
        {
            this.webTitikshaDbContext = webTitikshaDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await webTitikshaDbContext.AddAsync(region);
            webTitikshaDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await webTitikshaDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); 
            if(region == null)
            {
                return null;
            }

            //delete the region 
            webTitikshaDbContext.Regions.Remove(region);
            webTitikshaDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<IEnumerable<Region>>GetAllAsync()
        {
           return  await webTitikshaDbContext.Regions.ToListAsync();

        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await webTitikshaDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            
            
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await webTitikshaDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingregion == null)
            {
                return null;
            }
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.Area = region.Area;
            existingregion.Lat = region.Lat;
            existingregion.Long = region.Long;
            existingregion.Population = region.Population;

            await webTitikshaDbContext.SaveChangesAsync();
            return existingregion; 

        }
    }
}
