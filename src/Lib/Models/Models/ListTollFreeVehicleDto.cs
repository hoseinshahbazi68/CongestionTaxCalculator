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
    public class ListTollFreeVehicleDto  
    {
        #region لیست فیلد ها

        /// <summary>
        ///   شهر
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        ///   وسیله نقلیه
        /// </summary>
        public int VehicleId { get; set; }
        #endregion
    }
}
