using Entities.Vehicle;
using Models.Base;

namespace Models.Models
{
    public class ListCityConfigDto : BaseDto<ListCityConfigDto, CityConfigEntity>
    {
        #region لیست فیلد ها

        /// <summary>
        /// شهر
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// تعطیلات چک شود
        /// </summary>
        public bool IsCheckHoliday { get; set; } = false;

        /// <summary>
        /// وسایل نقلیه رایگان چک شود
        /// برای اینکه به ازای هر وسیله دیتابیس چک نشود این فیلد اضافه شده است
        /// </summary>
        public bool IsCheckTollFreeVehicle { get; set; } = false;

        #endregion
    }
}
