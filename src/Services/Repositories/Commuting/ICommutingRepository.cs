using Entities.Vehicle;
using Models.Base;
using Models.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TaxCalculator
{
    public interface ICommutingRepository : IRepository<CommutingEntity>
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ListCommutingReportDto>>> GetAllAsync(int CityId);
    }
}
