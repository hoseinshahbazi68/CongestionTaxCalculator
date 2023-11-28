using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Vehicle
{
    /// <summary>
    /// وسایل نقلیه
    /// </summary>
    public interface IVehicleRepository : IRepository<VehicleEntity>
    {
        /// <summary>
        /// گرفتن لیست وسایل نقلیه
        /// </summary>
        /// <returns></returns>
        Task<List<ListVehicleDto>> GetAllAsync();
    }
}
