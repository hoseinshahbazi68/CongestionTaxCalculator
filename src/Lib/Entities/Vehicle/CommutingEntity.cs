using DNTPersianUtils.Core.IranCities;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Vehicle
{
    [Table("Commuting")]
    public class CommutingEntity : BaseEntity<long>
    {
        #region لیست فیلد ها
        /// <summary>
        /// وسیله 
        /// </summary>
        [Required]
        public int VehicleId { get; set; }

        /// <summary>
        /// شهر 
        /// </summary>
        [Required]
        public int CityId { get; set; }

        /// <summary>
        /// زمان ثبت شده
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        #endregion

        #region لیست ارتباطات
        /// <summary>
        /// ارتباط با وسیله  
        /// </summary>
        [InverseProperty(nameof(VehicleEntity.Commutings))]
        [ForeignKey(nameof(VehicleId))]
        public virtual VehicleEntity Vehicle { get; set; }


        /// <summary>
        /// ارتباط با شهر  
        /// </summary>
        [InverseProperty(nameof(CityEntity.Commutings))]
        [ForeignKey(nameof(CityId))]
        public virtual CityEntity City { get; set; }

        #endregion

    }
}
