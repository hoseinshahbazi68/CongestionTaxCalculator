using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.CityConfig
{
    public interface ICityConfigRepository : IRepository<CityConfigEntity>
    {
        /// <summary>
        /// گرفتن لیست تنظیمات شهر
        /// </summary>
        /// <returns></returns>
        Task<List<ListCityConfigDto>> GetAllAsync(CancellationToken cancellation);

    }
}
