using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.TollFreeVehicle
{
    public interface ITollFreeVehicleRepository : IRepository<TollFreeVehicleEntity>
    {
        /// <summary>
        /// گرفتن لیست وسایل نقلیه رایگان
        /// </summary>
        /// <returns></returns>
        Task<List<ListTollFreeVehicleDto>> GetAllAsync();

    }
}
