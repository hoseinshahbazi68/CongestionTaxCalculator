using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    [Table("CityConfig")]
    public class CityConfigEntity : BaseEntity
    {
        #region لیست فیلد ها

        /// <summary>
        /// شهر
        /// </summary>
        [Range(1, int.MaxValue)]
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


        #region لیست ارتباطات

        [InverseProperty(nameof(CityEntity.CityConfigs))]
        [ForeignKey(nameof(CityId))]
        public virtual CityEntity City { get; set; }

        #endregion
    }
}
