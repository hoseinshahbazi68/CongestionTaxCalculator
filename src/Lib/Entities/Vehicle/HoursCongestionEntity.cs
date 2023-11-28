using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    [Table("HoursCongestion")]
    public class HoursCongestionEntity : BaseEntity
    {
        #region لیست فیلد ها

        /// <summary>
        ///   شهر
        /// </summary>
        [Range(1, int.MaxValue)]
        public int CityId { get; set; }

        /// <summary>
        /// ساعت شروع   
        /// </summary>
        [Required]
        public TimeSpan TimeStart { get; set; }
        /// <summary>
        /// ساعت پایان   
        /// </summary>
        [Required]
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// مبلغ
        /// </summary>
        [Required]
        public double Fee { get; set; }

        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(CityEntity.HoursCongestions))]
        [ForeignKey(nameof(CityId))]
        public virtual CityEntity City { get; set; }
        #endregion
    }
}
