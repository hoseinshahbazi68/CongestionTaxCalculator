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
