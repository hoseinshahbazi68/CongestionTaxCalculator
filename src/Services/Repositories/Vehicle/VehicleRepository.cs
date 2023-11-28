using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Vehicle
{
    public class VehicleRepository : Repository<VehicleEntity>, IVehicleRepository, IScopedDependency
    {
        public VehicleRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// گرفتن لیست وسایل نقلیه
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListVehicleDto>> GetAllAsync() => await TableNoTracking.ProjectTo<ListVehicleDto>(Mapper.ConfigurationProvider).ToListAsync();
       
    }
}
