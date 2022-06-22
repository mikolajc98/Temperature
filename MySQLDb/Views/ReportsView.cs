using MySQLDb.Extensions;
using MySQLDb.Models;
using System;

namespace MySQLDb.Views
{
    public class ReportsView : BaseConsoleView
    {
        private readonly Report _report;

        public ReportsView(BaseConsoleView _previousView)
           : base(_previousView)
        {
            _report = new Report();
        }

        public override void Open()
        {
            base.Open();
            Console.WriteLine("Sekcja raportowania. Co chcesz zrobić?");
            Console.WriteLine("1. Podaj średnią temperaturę każdego pokoju");
            Console.WriteLine("2. Podaj najwyższą temperaturę każdego pokoju");
            Console.WriteLine("3. Podaj najniższą temperaturę każdego pokoju");
            Console.WriteLine("4. Powrót");

            switch (ConsoleExtensions.GetValue<int>("Nie udało się. Wprowadź wartość jeszcze raz"))
            {
                case 1:
                    ShowAverage();
                    break;
                case 2:
                    ShowMax();
                    break;
                case 3:
                    ShowMin();
                    break;
                case 4:
                    GoBack();
                    break;
            }
            Open();
        }

        private void ShowAverage()
        {
            _report.GetAverageForEachRoom();

            ConsoleExtensions.PressEnterToReturn();
            return;
        }

        private void ShowMax()
        {
            _report.GetMaxForEachRoom();

            ConsoleExtensions.PressEnterToReturn();
            return;
        }

        private void ShowMin()
        {
            _report.GetMinForEachRoom();

            ConsoleExtensions.PressEnterToReturn();
            return;
        }
    }
}
