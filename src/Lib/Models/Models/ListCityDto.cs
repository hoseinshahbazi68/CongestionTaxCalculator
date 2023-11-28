using Entities.Vehicle;
using Models.Base;

namespace Models.Models
{
    public class ListCityDto : BaseDto<ListCityDto, CityEntity>
    {
        #region لیست فیلد ها

        /// <summary>
        /// نام شهر 
        /// </summary>
        public string Title { get; set; }
        #endregion
    }
}
