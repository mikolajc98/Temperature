using MySQLDb.Views;
using System;
using System.Collections.Generic;

namespace MySQLDb.Extensions
{
    public static class ConsoleExtensions
    {
        private delegate void HandledCommand();
        private static BaseConsoleView _currentConsoleContext;
        private readonly static Dictionary<string, HandledCommand> _handledCommands;

        static ConsoleExtensions()
        {
            Console.Title = "Temperature manager";


            _handledCommands = new Dictionary<string, HandledCommand>
            {
                { "cls", () => { Console.Clear(); } },
                { "reopen", () => { _currentConsoleContext.Open(); } },
                { "help", () => { Console.WriteLine($"Konsola obsługuje 3 komendy.\r\ncls - służy do wyczyszczenia całej zawartości tekstu z konsoli.\r\nreopen - powoduje wyświetlenie jeszcze raz okna menu kontekstu, w jakim obecnie się znajduje.\r\nhelp - wywołanie tej komendy."); } }
            };
        }

        public static void SetContext(BaseConsoleView view)
        {
            //Console.WriteLine($"Context set to {view.GetType().Name}");
            _currentConsoleContext = view;
        }       

        public static T GetValue<T>(string errorMessage) where T : IComparable<T>
        {    
            try
            {
                return (T)Convert.ChangeType(ReadLine(), typeof(T));
            }
            catch (Exception)
            {
                Console.WriteLine(errorMessage);
                return GetValue<T>(errorMessage);
            }
        }

        public static void DisplayList<T>(List<T> list, int itemsPerLine) 
        {    
            for(int i = 0; i < list.Count; i++)
            {                
                if (i % itemsPerLine == 0)
                    Console.WriteLine();
                else
                    Console.Write('\t');

                Console.Write(list[i].ToString());
            }
            Console.WriteLine();
        }

        public static void PressEnterToReturn()
        {
            Console.WriteLine("Kliknij Enter, aby powrócić.");
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey();
            }
        }

        public static string ReadLine()
        {
            var line = Console.ReadLine();

            if (_handledCommands.ContainsKey(line))
            {
                _handledCommands[line].Invoke();
                return ReadLine();
            }
            return line;
        }
    }
}
