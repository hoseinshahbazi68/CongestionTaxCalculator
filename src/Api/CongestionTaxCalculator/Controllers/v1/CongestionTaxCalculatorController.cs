using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.TaxCalculator;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    /// محاسبه مالیات
    /// </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CongestionTaxCalculatorController : BaseController
    {
        private readonly ICommutingRepository _congestionTaxCalculatorRepository;
        public CongestionTaxCalculatorController(ICommutingRepository congestionTaxCalculatorRepository)
        {
            _congestionTaxCalculatorRepository = congestionTaxCalculatorRepository;
        }

        /// <summary>
        /// گرفتن مالیات وسایل نقلیه
        /// </summary>
        /// <returns></returns>
        [HttpPost("Get/{CityId:int}")]
        public async Task<object> Get(int CityId) => await _congestionTaxCalculatorRepository.GetAllAsync(CityId);
    }
}
