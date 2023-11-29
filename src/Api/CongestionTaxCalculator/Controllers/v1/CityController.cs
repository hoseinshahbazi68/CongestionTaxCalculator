using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.City;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    /// شهرها
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CityController : BaseController
    {
        private readonly ICityRepository _CityRepository;
        public CityController(ICityRepository CityRepository)
        {
            _CityRepository = CityRepository;
        }

        /// <summary>
        /// گرفتن لیست شهرها
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Get(CancellationToken cancellation) => await _CityRepository.GetAllAsync(cancellation);
    }
}
