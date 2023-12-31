﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.CityConfig;
using Repositories.Holiday;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    /// تعطیلات  
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class HolidayController : BaseController
    {
        private readonly IHolidayRepository _HolidayRepository;
        public HolidayController(IHolidayRepository HolidayRepository)
        {
            _HolidayRepository = HolidayRepository;
        }

        /// <summary>
        /// گرفتن لیست   تعطیلات
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Get(CancellationToken cancellation) => await _HolidayRepository.GetAllAsync(cancellation);
    }
}
