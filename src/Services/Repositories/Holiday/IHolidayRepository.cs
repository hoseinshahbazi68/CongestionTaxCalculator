using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Holiday
{
    public interface IHolidayRepository : IRepository<HolidayEntity>
    {
        /// <summary>
        /// گرفتن لیست   تعطیلات
        /// </summary>
        /// <returns></returns>
        Task<List<ListHolidayDto>> GetAllAsync(CancellationToken cancellation);

    }
}
