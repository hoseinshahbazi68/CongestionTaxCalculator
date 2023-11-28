using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.City
{
    public class CityRepository : Repository<CityEntity>, ICityRepository, IScopedDependency
    {
        public CityRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// گرفتن لیست شهرها  
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListCityDto>> GetAllAsync() => await TableNoTracking.ProjectTo<ListCityDto>(Mapper.ConfigurationProvider).ToListAsync();

    }
}
