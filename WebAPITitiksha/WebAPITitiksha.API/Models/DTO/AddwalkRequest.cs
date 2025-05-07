using WebAPITitiksha.API.Models.Domain;

namespace WebAPITitiksha.API.Models.DTO
{
    public class AddwalkRequest
    {
        
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        
    }
}

