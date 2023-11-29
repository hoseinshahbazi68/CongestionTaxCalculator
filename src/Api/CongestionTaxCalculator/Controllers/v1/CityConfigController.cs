using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.City;
using Repositories.CityConfig;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    /// تنظیمات شهرها
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CityConfigController : BaseController
    {
        private readonly ICityConfigRepository _CityConfigRepository;
        public CityConfigController(ICityConfigRepository  CityConfigRepository)
        {
            _CityConfigRepository = CityConfigRepository;
        }

        /// <summary>
        /// گرفتن لیست تنظیمات شهرها
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Get() => await _CityConfigRepository.GetAllAsync();
    }
}
