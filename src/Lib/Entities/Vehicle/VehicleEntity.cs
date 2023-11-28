using Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    [Table("Vehicle")]
    public class VehicleEntity : BaseEntity
    {
        #region لیست فیلد ها

        /// <summary>
        /// نام وسیله 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        #endregion

        #region لیست ارتباطات
        /// <summary>
        /// ارتباط با  وسایل نقلیه رایگان
        /// </summary>
        [InverseProperty(nameof(TollFreeVehicleEntity.Vehicle))]
        public virtual ICollection<TollFreeVehicleEntity> TollFreeVehicles { get; set; }


        /// <summary>
        /// ارتباط با   رفت و آمد    
        /// </summary>
        [InverseProperty(nameof(CommutingEntity.Vehicle))]
        public virtual ICollection<CommutingEntity> Commutings { get; set; }

        #endregion
    }
}
