using System;
using System.Linq;

namespace MySQLDb.Models
{
    public class Report
    {
        private readonly ReadingRepository _readingRepository;

        public Report()
        {
            _readingRepository = new ReadingRepository();
        }

        public void GetAverageForEachRoom()
        {
            var allReadings = _readingRepository.GetAll();

            var groupedByRoom = allReadings.ToList().GroupBy(x => x.Room).ToList();

            foreach(var groupItem in groupedByRoom)
            {
                var room = groupItem.Key;

                var average = groupItem.Select(x => x.TemperatureObj).Average(y => y.GetTemperature(TemperatureUnit.Celsius));

                Console.WriteLine($"{room.Name}. Średnia: {average} °C");
            }
        }

        public void GetMaxForEachRoom()
        {
            var allReadings = _readingRepository.GetAll();

            var groupedByRoom = allReadings.ToList().GroupBy(x => x.Room);

            foreach (var groupItem in groupedByRoom)
            {
                var room = groupItem.Key;

                var average = groupItem.Select(x => x.TemperatureObj).Max(y => y.GetTemperature(TemperatureUnit.Celsius));

                Console.WriteLine($"{room.Name}. Maksymalna: {average} °C");
            }
        }

        public void GetMinForEachRoom()
        {
            var allReadings = _readingRepository.GetAll();

            var groupedByRoom = allReadings.ToList().GroupBy(x => x.Room);

            foreach (var groupItem in groupedByRoom)
            {
                var room = groupItem.Key;

                var average = groupItem.Select(x => x.TemperatureObj).Min(y => y.GetTemperature(TemperatureUnit.Celsius));

                Console.WriteLine($"{room.Name}. Minimalna: {average} °C");
            }
        }
    }
}
