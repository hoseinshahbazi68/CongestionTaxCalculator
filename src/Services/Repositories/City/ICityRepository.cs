using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.City
{
    public interface ICityRepository : IRepository<CityEntity>
    {
        /// <summary>
        /// گرفتن لیست شهرها
        /// </summary>
        /// <returns></returns>
        Task<List<ListCityDto>> GetAllAsync();

    }
}
