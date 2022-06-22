using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLDb.Models
{
    public class Temperature
    {
        //Kelvin is the base temperature unit - it sits inside database.
        private decimal _kelvinDegree;        
        private decimal _celsius => _kelvinDegree - 273.15M;
        private decimal _fahrenheit => (_kelvinDegree * (9.0M / 5.0M)) - 459.68M;


        public Temperature(decimal degree, TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Kelvin:
                    _kelvinDegree = degree;
                    break;                    
                case TemperatureUnit.Fahrenheit:
                    _kelvinDegree = (degree + 459.68M) * (5.0M / 9.0M);
                    break;
                case TemperatureUnit.Celsius:
                    _kelvinDegree = degree + 273.15M;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown temperature unit.");
            }
        }

        public decimal GetTemperature(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Kelvin:
                    return _kelvinDegree;                    
                case TemperatureUnit.Fahrenheit:
                    return _fahrenheit;
                case TemperatureUnit.Celsius:
                    return _celsius;
                default:
                    return 0;
            }
        }

        public string GetTemperatureWithUnit(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Kelvin:
                    return $"{_kelvinDegree} K";
                case TemperatureUnit.Fahrenheit:
                    return $"{_fahrenheit} °F";
                case TemperatureUnit.Celsius:
                    return $"{_celsius} °C";
                default:
                    return "-";
            }
        }
    }

    public enum TemperatureUnit
    {
        Kelvin,
        Fahrenheit,
        Celsius
    }
}
