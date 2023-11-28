using Entities.Vehicle;
using Models.Base;

namespace Models.Models
{
    public class ListCommutingDto : BaseDto<ListCommutingDto, CommutingEntity, long>
    {
        #region لیست فیلد ها

        /// <summary>
        /// شهر
        /// </summary>
        public int CityId { get; set; }


        #endregion
    }


    public class ListCommutingReportDto
    {
        public string Title { get; set; }
        public double Fee { get; set; }

    }
}
