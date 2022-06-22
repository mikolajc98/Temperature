using System;
using MySQLDb.Extensions;

namespace MySQLDb.Views
{
    public class MainView : BaseConsoleView
    {
        private readonly RoomView _roomView;
        private readonly ReadingView _readingView;
        private readonly ReportsView _reportsView;


        public MainView()
            : base(null)
        {
            _roomView = new RoomView(this);
            _readingView = new ReadingView(this);
            _reportsView = new ReportsView(this);  
        }

        public override void Open()
        {
            base.Open();            
            Console.WriteLine("Witaj w programie zarządzania temperaturą mieszkania. Wybierz sekcję, do której chciałbyś przejść:");
            Console.WriteLine("1. Pomieszczenia");
            Console.WriteLine("2. Odczyty");
            Console.WriteLine("3. Raporty");
            Console.WriteLine("4. Zamknij program");
            Console.Write("Wybór: ");
           

            switch (ConsoleExtensions.GetValue<int>("Nie udało się. Wprowadź wartość jeszcze raz"))
            {
                case 1:
                    _roomView.Open();
                    break;
                case 2:
                    _readingView.Open();
                    break;
                case 3:
                    _reportsView.Open();
                    break;
                case 4:
                    Console.WriteLine("Zakończono pracę programu. Kliknij dowolny przycisk aby zamknąć.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    return;
            }
        }
    }
}
