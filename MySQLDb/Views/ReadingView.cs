using MySQLDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MySQLDb.Extensions;

namespace MySQLDb.Views
{
    public class ReadingView : BaseConsoleView
    {
        private readonly RoomRepository _roomRepository;
        private readonly ReadingRepository _readingRepository;

        public ReadingView(BaseConsoleView _previousView)
            : base(_previousView)
        {
            _roomRepository = new RoomRepository();
            _readingRepository = new ReadingRepository();
        }

        public override void Open()
        {
            base.Open();
            Console.WriteLine("Sekcja odczytów. Co chcesz zrobić?");
            Console.WriteLine("1. Wypisz wszystkie odczyty");
            Console.WriteLine("2. Dodaj nowy odczyt");
            Console.WriteLine("3. Usuń odczyt.");            
            Console.WriteLine("4. Powrót");
            Console.Write("Wybór: ");

            switch (ConsoleExtensions.GetValue<int>("Nie udało się. Wprowadź wartość jeszcze raz"))
            {
                case 1:
                    ListAllReadings();
                    break;
                case 2:
                    AddNewReading();
                    break;
                case 3:
                    RemoveReading();
                    break;
                case 4:
                    GoBack();
                    break;
            }

            Open();
        }

        private void ListAllReadings()
        {
            var readings = _readingRepository.GetAll().ToList();

            Console.WriteLine("Numer odczytu \t Pomieszczenie \t Odczytana temperatura\t Data odczytu");

            ConsoleExtensions.DisplayList(readings,1);

            ConsoleExtensions.PressEnterToReturn();
            return;
        }

        private void AddNewReading()
        {
            var allRooms = _roomRepository.GetAll();

            ConsoleExtensions.DisplayList(allRooms.ToList(),1);

            Console.Write("Podaj numer pomieszczenia: ");
            var roomId = ConsoleExtensions.GetValue<int>("Nie udało się. Wprowadź wartość jeszcze raz");

            var selectedRoom = _roomRepository.Get(roomId);

            if (selectedRoom is null)
            {
                Console.WriteLine("Nie znaleziono pomieszczenia o takim numerze.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }

            Console.WriteLine("W jakich jednostkach podasz temperaturę?");
            Console.WriteLine("K - Kelwiny");
            Console.WriteLine("F - Fahrenheity");
            Console.WriteLine("C - Celsjusze");
            var option = ConsoleExtensions.ReadLine().ToUpper();

            var availableValues = new List<string>
            { "K","F","C" };

            while (availableValues.Contains(option) == false)
            {
                Console.WriteLine("Nie podano wartości z listy. Spróbuj ponownie");
                option = ConsoleExtensions.ReadLine().ToUpper();
            }

            var temperatureUnit = TemperatureUnit.Celsius;

            switch(option)
            {
                case "K":
                    temperatureUnit = TemperatureUnit.Kelvin;
                    break;
                case "F":
                    temperatureUnit = TemperatureUnit.Fahrenheit;
                    break;
                case "C":
                    temperatureUnit = TemperatureUnit.Celsius;
                    break;
            }

            Console.Write("Podaj temperaturę w wybranej jednostce: ");
            var temperatureReading = ConsoleExtensions.GetValue<decimal>("Nieprawidłowe podanie temperatury. Spróbuj ponownie.");

            var temperature = new Temperature(temperatureReading, temperatureUnit);


            var newReading = new Reading(selectedRoom, temperature, DateTime.Now);

            var addEffect = _readingRepository.Add(newReading);

            if (addEffect)
            {
                Console.WriteLine("Udało się dodać odczyt temperatury.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
            else
            {
                Console.WriteLine("Nie udało się dodać odczytu.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
        }

        private void RemoveReading()
        {
            Console.Write("Wprowadź numer odczytu do usunięcia: ");
            var readingId = ConsoleExtensions.GetValue<int>("Nie wprowadzono wartości liczbowej. Spróbuj ponownie.");

            var readingToRemove = _readingRepository.Get(readingId);

            if(readingToRemove is null)
            {
                Console.WriteLine("Nie znaleziono odczytu o takim numerze.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }

            var removeEffect = _readingRepository.Remove(readingToRemove);

            if (removeEffect)
            {
                Console.WriteLine("Udało się usunąć odczyt.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
            else
            {
                Console.WriteLine("Nie udało się usunąć odczytu.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }

        }
    }
}
