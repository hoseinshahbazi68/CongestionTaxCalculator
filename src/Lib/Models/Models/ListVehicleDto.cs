using Entities.Vehicle;
using Models.Base;

namespace Models.Models
{
    public class ListVehicleDto : BaseDto<ListVehicleDto, VehicleEntity>
    {
        /// <summary>
        /// نام وسیله
        /// </summary>
        public string Title { get; set; }
    }
}
