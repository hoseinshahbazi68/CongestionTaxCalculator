using AutoMapper;
using Common;
using Data;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Models.Base;
using Models.Models;
using Repositories.Base;
using Repositories.City;
using Repositories.CityConfig;
using Repositories.Holiday;
using Repositories.TollFreeVehicle;
using Repositories.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TaxCalculator
{
    public class CommutingRepository : Repository<CommutingEntity>, ICommutingRepository, IScopedDependency
    {

        private readonly ICityConfigRepository _CityConfig;
        private readonly IHolidayRepository _Holiday;
        private readonly IHoursCongestionRepository _HoursCongestion;
        private readonly ITollFreeVehicleRepository _tollFreeVehicleRepository;
        List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>() { DayOfWeek.Sunday, DayOfWeek.Saturday };

        public CommutingRepository(ApplicationDbContext dbContext, IMapper mapper, ICityConfigRepository CityConfig,
            IHolidayRepository Holiday, IHoursCongestionRepository HoursCongestion, ITollFreeVehicleRepository tollFreeVehicleRepository)
            : base(dbContext, mapper)
        {
            _CityConfig = CityConfig;
            _Holiday = Holiday;
            _HoursCongestion = HoursCongestion;
            _tollFreeVehicleRepository = tollFreeVehicleRepository;
        }

        public async Task<ApiResult<List<ListCommutingReportDto>>> GetAllAsync(int CityId)
        {
            var CityConfig = await _CityConfig.TableNoTracking.SingleOrDefaultAsync(x => x.CityId.Equals(CityId));
            if (CityConfig is null)
                return new ApiResult<List<ListCommutingReportDto>>(false, ApiResultStatusCode.NotFound, null, "تنظیمات شهر یافت نشد");

            var HoursCongestion = await _HoursCongestion.TableNoTracking.Where(x => x.CityId == CityId).ToListAsync();


            var data = await TableNoTracking
                .Where(x =>

                x.CityId == CityId

                 && (CityConfig.IsCheckHoliday ? !_Holiday.TableNoTracking.Any(h => h.Date == x.Date)
                  : true)

                 && (CityConfig.IsCheckTollFreeVehicle ? !_tollFreeVehicleRepository.TableNoTracking.Any(tfv => tfv.CityId == x.CityId && tfv.VehicleId == x.VehicleId) : true)

                ).GroupBy(x => x.VehicleId)
                .Select(x => new ListCommutingReportDto()
                {
                    Title = x.First().Vehicle.Title,
                    Fee = GetTax(x.Select(d => d.Date).ToArray(), HoursCongestion, CityConfig.IsCheckHoliday)
                }).ToListAsync();


            return data;
        }




        ///دریافت مالیات
        public double GetTax(DateTime[] dates, List<HoursCongestionEntity> HoursCongestions, bool IsCheckHoliday)
        {
            if (dates.Length == 0)
                return 0;


            DateTime intervalStart = dates[0];
            double totalFee = 0;
            foreach (DateTime date in dates)
            {
                double nextFee = 0;
                double tempFee = 0;
                if (!IsCheckHoliday && !dayOfWeeks.Any(d => d == date.DayOfWeek))
                    nextFee = GetTollFee(date, HoursCongestions);

                if (!IsCheckHoliday && !dayOfWeeks.Any(d => d == intervalStart.DayOfWeek))
                    tempFee = GetTollFee(intervalStart, HoursCongestions);

                double minutes = (date - intervalStart).TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                    totalFee += nextFee;

                intervalStart = date;
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        /// <summary>
        /// دریافت هزینه عوارض
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public double GetTollFee(DateTime date, List<HoursCongestionEntity> HoursCongestions)
        {
            TimeSpan Time = date.TimeOfDay;

            var hc = HoursCongestions.Where(x => Time >= x.TimeStart && Time <= x.TimeEnd).FirstOrDefault();

            if (hc is null)
                return 0;

            return hc.Fee;

        }

    }
}
