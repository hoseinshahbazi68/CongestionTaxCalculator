using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Vehicle
{
    [Table("Holiday")]
    public class HolidayEntity : BaseEntity
    {
        #region لیست فیلد ها
        /// <summary>
        ///تاریخ 
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        #endregion
    }
}
