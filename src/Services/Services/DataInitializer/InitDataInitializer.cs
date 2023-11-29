using Bogus.DataSets;
using DNTPersianUtils.Core.IranCities;
using Entities.Vehicle;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.DataInitializer
{
    public class InitDataInitializer : IDataInitializer
    {
        private readonly IRepository<CityEntity> _City;
        private readonly IRepository<CityConfigEntity> _CityConfig;
        private readonly IRepository<HolidayEntity> _HolidayEntity;
        private readonly IRepository<VehicleEntity> _Vehicle;
        private readonly IRepository<TollFreeVehicleEntity> _TollFreeVehicle;
        private readonly IRepository<HoursCongestionEntity> _HoursCongestion;
        private readonly IRepository<CommutingEntity> _Commuting;

        public InitDataInitializer(IRepository<CityEntity> City, IRepository<CityConfigEntity>
            cityConfig, IRepository<HolidayEntity> holidayEntity, IRepository<VehicleEntity> vehicle,
            IRepository<TollFreeVehicleEntity> tollFreeVehicle, IRepository<HoursCongestionEntity> hoursCongestion,
            IRepository<CommutingEntity> commuting)
        {
            _City = City;
            _CityConfig = cityConfig;
            _HolidayEntity = holidayEntity;
            _Vehicle = vehicle;
            _TollFreeVehicle = tollFreeVehicle;
            _HoursCongestion = hoursCongestion;
            _Commuting = commuting;
        }

        public void InitializeData()
        {
            if (!_City.TableNoTracking.Any(p => p.Title == "Gothenburg"))
            {
                var city = new CityEntity()
                {
                    Title = "Gothenburg",
                };

                _City.Add(city);

                _CityConfig.Add(new CityConfigEntity()
                {
                    City = city,
                    IsCheckHoliday = true,
                    IsCheckTollFreeVehicle = true,
                });

                List<VehicleEntity> Vehicles = new() {
                    new VehicleEntity() { Title="Car" },
                    new VehicleEntity() { Title="Motorcycle" },
                    new VehicleEntity() { Title="Tractor" },
                    new VehicleEntity() { Title="Emergency" },
                    new VehicleEntity() { Title="Diplomat" },
                    new VehicleEntity() { Title="Foreign" },
                    new VehicleEntity() { Title="Military" },
                };
                _Vehicle.AddRange(Vehicles);

                List<TollFreeVehicleEntity> TollFreeVehicles = new() {
                    new TollFreeVehicleEntity() {  City=city, VehicleId=2 },
                    new TollFreeVehicleEntity() {  City=city, VehicleId=3 },
                    new TollFreeVehicleEntity() {  City=city, VehicleId=4 },
                    new TollFreeVehicleEntity() {  City=city, VehicleId=5 },
                    new TollFreeVehicleEntity() {  City=city, VehicleId=6 },
                    new TollFreeVehicleEntity() {  City=city, VehicleId=7 },
                };

                _TollFreeVehicle.AddRange(TollFreeVehicles);

                List<HolidayEntity> holidays = new() {
                    new HolidayEntity() { Date = DateTime.Parse("2013-1-1") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-3-28") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-3-29") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-4-1") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-4-30") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-5-2") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-5-8") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-5-9") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-6-5") },
                    new HolidayEntity() { Date = DateTime.Parse("2013-6-6") },
                };
                _HolidayEntity.AddRange(holidays);

                List<HoursCongestionEntity> HoursCongestions = new() {
                    new  HoursCongestionEntity() { City =city , Fee=8 ,  TimeStart=TimeSpan.Parse("06:00"),TimeEnd=TimeSpan.Parse("06:29")  },
                    new  HoursCongestionEntity() { City =city , Fee=13 ,  TimeStart=TimeSpan.Parse("06:30"),TimeEnd=TimeSpan.Parse("06:59")  },
                    new  HoursCongestionEntity() { City =city , Fee=18 ,  TimeStart=TimeSpan.Parse("07:00"),TimeEnd=TimeSpan.Parse("07:59")  },
                    new  HoursCongestionEntity() { City =city , Fee=13 ,  TimeStart=TimeSpan.Parse("08:00"),TimeEnd=TimeSpan.Parse("08:29")  },
                    new  HoursCongestionEntity() { City =city , Fee=8 ,  TimeStart=TimeSpan.Parse("08:30"),TimeEnd=TimeSpan.Parse("14:59")  },
                    new  HoursCongestionEntity() { City =city , Fee=13 ,  TimeStart=TimeSpan.Parse("15:00"),TimeEnd=TimeSpan.Parse("15:29")  },
                    new  HoursCongestionEntity() { City =city , Fee=18 ,  TimeStart=TimeSpan.Parse("15:30"),TimeEnd=TimeSpan.Parse("16:59")  },
                    new  HoursCongestionEntity() { City =city , Fee=13 ,  TimeStart=TimeSpan.Parse("17:00"),TimeEnd=TimeSpan.Parse("17:59")  },
                    new  HoursCongestionEntity() { City =city , Fee=8 ,  TimeStart=TimeSpan.Parse("18:00"),TimeEnd=TimeSpan.Parse("18:29")  },
                    new  HoursCongestionEntity() { City =city , Fee=0 ,  TimeStart=TimeSpan.Parse("18:30"),TimeEnd=TimeSpan.Parse("05:59")  },
                };

                _HoursCongestion.AddRange(HoursCongestions);

                List<CommutingEntity> Commutings = new() {
                    new CommutingEntity() { Date = DateTime.Parse("2013-01-01 07:00:00.0000000"), City=city, VehicleId=1 },
                    new CommutingEntity() { Date = DateTime.Parse("2013-05-06 07:00:00.0000000"), City=city, VehicleId=1 },
                    new CommutingEntity() { Date = DateTime.Parse("2013-05-06 08:00:00.0000000"), City=city, VehicleId=1 },
                    new CommutingEntity() { Date = DateTime.Parse("2013-05-06 09:10:00.0000000"), City=city, VehicleId=1 },
                };

                _Commuting.AddRange(Commutings);



            }


        }
    }
}
