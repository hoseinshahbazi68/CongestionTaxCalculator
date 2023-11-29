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

namespace Repositories.City
{
    public class HoursCongestionRepository : Repository<HoursCongestionEntity>, IHoursCongestionRepository, IScopedDependency
    {
        public HoursCongestionRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
        /// گرفتن لیست ساعت ازدحام
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListHoursCongestionDto>> GetAllAsync(CancellationToken cancellation) => await TableNoTracking.ProjectTo<ListHoursCongestionDto>(Mapper.ConfigurationProvider).ToListAsync(cancellation);
    }
}
