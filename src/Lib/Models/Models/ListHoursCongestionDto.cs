using Entities.Vehicle;
using Models.Base;
using System;

namespace Models.Models
{
    public class ListHoursCongestionDto : BaseDto<ListHoursCongestionDto, HoursCongestionEntity>
    {
        #region لیست فیلد ها

        /// <summary>
        ///   شهر
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// ساعت شروع   
        /// </summary>
        public TimeSpan TimeStart { get; set; }
        /// <summary>
        /// ساعت پایان   
        /// </summary>
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// مبلغ
        /// </summary>
        public double Fee { get; set; }

        #endregion
    }
}
