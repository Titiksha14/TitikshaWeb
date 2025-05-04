using WebAPITitiksha.API.Models.Domain;

namespace WebAPITitiksha.API.Repository
{
    public interface IRegionRepository
    {
       Task <IEnumerable<Region>> GetAllAsync();

    }
}
