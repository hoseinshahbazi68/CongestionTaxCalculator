using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    /// <summary>
    /// وسیله های رایگان براساس شهر در این جدول قرار میگرند 
    /// </summary>
    [Table("TollFreeVehicle")]
    public class TollFreeVehicleEntity : IEntity
    {
        #region لیست فیلد ها

        /// <summary>
        ///   شهر
        /// </summary>
        [Range(1, int.MaxValue)]
        public int CityId { get; set; }
        /// <summary>
        ///   وسیله نقلیه
        /// </summary>
        [Range(1, int.MaxValue)]
        public int VehicleId { get; set; }
        #endregion
        #region لیست ارتباطات
        [InverseProperty(nameof(CityEntity.TollFreeVehicles))]
        [ForeignKey(nameof(CityId))]
        public virtual CityEntity City { get; set; }


        [InverseProperty(nameof(VehicleEntity.TollFreeVehicles))]
        [ForeignKey(nameof(VehicleId))]
        public virtual VehicleEntity Vehicle { get; set; }

        #endregion 
    }
}
