using WebAPITitiksha.API.Data;
using WebAPITitiksha.API.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace WebAPITitiksha.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WebTitikshaDbContext webTitikshaDbContext;

        public RegionRepository(WebTitikshaDbContext webTitikshaDbContext)
        {
            this.webTitikshaDbContext = webTitikshaDbContext;
        }

        

        public async Task<IEnumerable<Region>>GetAllAsync()
        {
           return  await webTitikshaDbContext.Regions.ToListAsync();

        }
    }
}
