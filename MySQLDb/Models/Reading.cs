using System;

namespace MySQLDb.Models
{
    public class Reading : Entity
    {
        protected Reading()
        {}

        public Reading(Room room, Temperature temperature, DateTime readingDate)
        {            
            Room = room;
            TemperatureObj = temperature;
            Date = readingDate;
        }

        public Room Room { get; set; }
        public Temperature TemperatureObj { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Dapper use only!
        /// </summary>
        public decimal Temperature
        {
            get => TemperatureObj.GetTemperature(TemperatureUnit.Kelvin);
            set => TemperatureObj = new Temperature(value, TemperatureUnit.Kelvin);
        }

        public override string ToString()
        {
            return $"{Id} - {Room.Name} - {TemperatureObj.GetTemperatureWithUnit(TemperatureUnit.Celsius)} - {Date.ToShortDateString()} {Date.ToShortTimeString()}";
        }
    }
}
