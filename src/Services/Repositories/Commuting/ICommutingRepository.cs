using Entities.Vehicle;
using Models.Base;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.TaxCalculator
{
    public interface ICommutingRepository : IRepository<CommutingEntity>
    {
        /// <summary>
        /// محاسبه مالیات
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ListCommutingReportDto>>> GetAllAsync(int CityId, CancellationToken cancellation);
    }
}
