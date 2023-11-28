using Entities.Vehicle;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
