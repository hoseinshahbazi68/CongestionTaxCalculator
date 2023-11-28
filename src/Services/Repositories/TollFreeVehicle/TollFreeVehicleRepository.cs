using AutoMapper;
using Common;
using Data;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.TollFreeVehicle
{
    /// <summary>
    /// وسابل نقلیه رایگان
    /// </summary>
    public class TollFreeVehicleRepository : Repository<TollFreeVehicleEntity>, ITollFreeVehicleRepository, IScopedDependency
    {
        public TollFreeVehicleRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        /// <summary>
        /// گرفتن لیست وسایل نقلیه رایگان
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListTollFreeVehicleDto>> GetAllAsync()
        {
            return await TableNoTracking.Select(x => new ListTollFreeVehicleDto() { CityId = x.CityId, VehicleId = x.VehicleId }).ToListAsync();
        }
    }
}
