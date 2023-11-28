using Entities.Vehicle;
using Models.Base;
using System;

namespace Models.Models
{
    public class ListHolidayDto : BaseDto<ListHolidayDto, HolidayEntity>
    {
        #region لیست فیلد ها
        /// <summary>
        ///تاریخ 
        /// </summary>
        public DateTime Date { get; set; }

        #endregion
    }
}
