using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.CityConfig
{
    public class CityConfigRepository : Repository<CityConfigEntity>, ICityConfigRepository, IScopedDependency
    {
        public CityConfigRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// گرفتن لیست تنظیمات  
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListCityConfigDto>> GetAllAsync(CancellationToken cancellation) => await TableNoTracking.ProjectTo<ListCityConfigDto>(Mapper.ConfigurationProvider).ToListAsync(cancellation);

    }
}