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

namespace Repositories.Holiday
{
    public class HolidayRepository : Repository<HolidayEntity>, IHolidayRepository, IScopedDependency
    {
        public HolidayRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// گرفتن لیست ساعت ازدحام
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListHolidayDto>> GetAllAsync(CancellationToken cancellation) => await TableNoTracking.ProjectTo<ListHolidayDto>(Mapper.ConfigurationProvider).ToListAsync(cancellation);
    }
}
