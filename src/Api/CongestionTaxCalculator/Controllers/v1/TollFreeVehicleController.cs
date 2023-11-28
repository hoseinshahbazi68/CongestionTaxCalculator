﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.TollFreeVehicle;
using Repositories.Vehicle;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    ///   وسایل نقلیه رایگان
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class TollFreeVehicleController : BaseController
    {
        private readonly ITollFreeVehicleRepository _tollFreeVehiclRepository;
        public TollFreeVehicleController(ITollFreeVehicleRepository tollFreeVehiclRepository)
        {
            _tollFreeVehiclRepository = tollFreeVehiclRepository;
        }

        /// <summary>
        /// گرفتن لیست وسایل نقلیه نقلیه رایگان
        /// </summary>
        /// <returns></returns>
        [HttpPost("Get/{CityId:int}")]
        public async Task<object> Get(int CityId) => await _tollFreeVehiclRepository.GetAllAsync();
    }
}
