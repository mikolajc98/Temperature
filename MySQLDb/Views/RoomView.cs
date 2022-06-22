using MySQLDb.Models;
using System;
using System.Linq;
using MySQLDb.Extensions;

namespace MySQLDb.Views
{
    public class RoomView : BaseConsoleView
    {
        private readonly RoomRepository _roomRepository;

        public RoomView(BaseConsoleView _previousView)
            : base(_previousView)
        {
            _roomRepository = new RoomRepository();
        }

        public override void Open()
        {
            base.Open();
            Console.WriteLine("Sekcja zarządzania pomieszczeniami. Co chcesz zrobić?");
            Console.WriteLine("1. Wypisz wszystkie pomieszczenia");
            Console.WriteLine("2. Dodaj nowe pomieszczenie");
            Console.WriteLine("3. Usuń pomieszczenie");
            Console.WriteLine("4. Powrót");
            Console.Write("Wybór: ");

            switch (ConsoleExtensions.GetValue<int>("Nie udało się. Wprowadź wartość jeszcze raz"))
            {
                case 1:
                    ListRooms();
                    break;
                case 2:
                    AddRoom();
                    break;
                case 3:
                    RemoveRoom();
                    break;
                case 4:
                    GoBack();
                    break;
            }
            Open();
        }


        private void AddRoom()
        {
            Console.Write("Podaj nazwę pomieszczenia: ");
            var roomName = ConsoleExtensions.ReadLine();

            var allRoomNames = _roomRepository.GetAll().Select(x => x.Name).ToList();

            if (allRoomNames.Contains(roomName, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Nazwa pomieszczenia już istnieje.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }

            var newRoom = new Room();

            var setEffect = newRoom.SetName(roomName);

            if (setEffect == false)
            {
                Console.WriteLine("Nie udało się ustawić wprowadzonej nazwy pomieszczenia.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }

            var added = _roomRepository.Add(newRoom);

            if (added)
            {
                Console.WriteLine("Udało się dodać pomieszczenie.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
            else
            {
                Console.WriteLine("Nie udało się dodać pomieszczenia.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
        }

        private void RemoveRoom()
        {
            Console.Write("Wpisz nazwę pomieszczenia, które chcesz usunąć:");
            var roomNameToRemove = ConsoleExtensions.ReadLine();

            var rooms = _roomRepository.GetAll();

            if (rooms.Select(x => x.Name).Contains(roomNameToRemove, StringComparer.OrdinalIgnoreCase))
            {
                var room = rooms.First(x => x.Name.ToLower() == roomNameToRemove.ToLower());

                var removeEffect = _roomRepository.Remove(room);

                if (removeEffect)
                {
                    Console.WriteLine("Udało się usunąć pomieszczenie.");
                    ConsoleExtensions.PressEnterToReturn();
                    return;
                }
                else
                {
                    Console.WriteLine("Nie udało się usunąć pomieszczenia.");
                    ConsoleExtensions.PressEnterToReturn();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono pomieszczenia o takiej nazwie.");
                ConsoleExtensions.PressEnterToReturn();
                return;
            }
        }

        private void ListRooms()
        {
            var rooms = _roomRepository.GetAll();
            Console.WriteLine("Numer pomieszczenia \t Nazwa");
            ConsoleExtensions.DisplayList(rooms.ToList(),2);

            ConsoleExtensions.PressEnterToReturn();
            return;
        }
    }
}
