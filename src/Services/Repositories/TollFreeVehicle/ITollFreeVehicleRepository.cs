using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.TollFreeVehicle
{
    public interface ITollFreeVehicleRepository : IRepository<TollFreeVehicleEntity>
    {
        /// <summary>
        /// گرفتن لیست وسایل نقلیه رایگان
        /// </summary>
        /// <param name="CityId">شماره شهر</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<ListTollFreeVehicleDto>> GetAllAsync(int CityId,CancellationToken cancellation);

    }
}
