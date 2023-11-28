using Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    [Table("City")]
    public class CityEntity : BaseEntity
    {
        #region لیست فیلد ها

        /// <summary>
        /// نام شهر 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        #endregion

        #region لیست ارتباطات
        /// <summary>
        /// ارتباط با  وسایل نقلیه رایگان
        /// </summary>
        [InverseProperty(nameof(TollFreeVehicleEntity.City))]
        public virtual ICollection<TollFreeVehicleEntity> TollFreeVehicles { get; set; }

        /// <summary>
        /// ارتباط با شهر  
        /// </summary>

        [InverseProperty(nameof(HoursCongestionEntity.City))]
        public virtual ICollection<HoursCongestionEntity> HoursCongestions { get; set; }

        /// <summary>
        /// ارتباط با شهر  
        /// </summary>

        [InverseProperty(nameof(CityConfigEntity.City))]
        public virtual ICollection<CityConfigEntity> CityConfigs { get; set; }

        /// <summary>
        /// ارتباط با شهر   
        /// </summary>
        [InverseProperty(nameof(CommutingEntity.City))]
        public virtual ICollection<CommutingEntity> Commutings { get; set; }

        #endregion
    }
}
