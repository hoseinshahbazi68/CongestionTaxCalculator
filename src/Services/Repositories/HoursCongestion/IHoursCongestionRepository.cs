using Entities.Vehicle;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.City
{
    public interface IHoursCongestionRepository : IRepository<HoursCongestionEntity>
    {
        /// <summary>
        /// گرفتن لیست ساعت ازدحام
        /// </summary>
        /// <returns></returns>
        Task<List<ListHoursCongestionDto>> GetAllAsync();
    }
}
