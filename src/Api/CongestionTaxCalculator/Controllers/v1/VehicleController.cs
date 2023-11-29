using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Vehicle;
using WebFramework.Api;

namespace CongestionTaxCalculator.Controllers.v1
{
    /// <summary>
    ///   وسایل نقلیه
    ///  </summary>
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class VehicleController : BaseController
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// گرفتن لیست وسایل نقلیه
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> Get(CancellationToken cancellation) => await _vehicleRepository.GetAllAsync(cancellation);

    }
}
