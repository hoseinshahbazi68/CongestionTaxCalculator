using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.City;
using Repositories.Holiday;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    /// ساعت ازدحام  
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class HoursCongestionController : BaseController
    {
        private readonly IHoursCongestionRepository  _HoursCongestionRepository;
        public HoursCongestionController(IHoursCongestionRepository  HoursCongestionRepository)
        {
            _HoursCongestionRepository = HoursCongestionRepository;
        }

        /// <summary>
        /// گرفتن لیست   ساعت ازدحام
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Get() => await _HoursCongestionRepository.GetAllAsync();
    }
}
