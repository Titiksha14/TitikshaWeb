using Microsoft.EntityFrameworkCore;
using WebAPITitiksha.API.Models.Domain;

namespace WebAPITitiksha.API.Data
{
    public class WebTitikshaDbContext:DbContext
    {
        public WebTitikshaDbContext(DbContextOptions<WebTitikshaDbContext>options):base (options) 
        {
            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk>Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
    }
}
