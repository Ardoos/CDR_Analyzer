using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    public static class MessageController
    {
        public static void HelloMessage()
        {
            Console.WriteLine("*** Witaj w programie analizującym pliki CDR ***");
        }

        public static bool StartLoadData()
        {
            Console.WriteLine("Baza jest pusta, wciśnij \"T\", aby wczytać dane z domyślnego pliku");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.T)
                return true;
            return false;
        }

        public static void MainMenuMessage()
        {
            Console.WriteLine("\nCo chcesz teraz zrobić?");
            Console.WriteLine("Wciśnij F, aby przefitrować dane");
            Console.WriteLine("Wciśnij W, aby wyświetlić obecne informacje o aktualnych danych");
            Console.WriteLine("Wciśnij R, aby ponownie wczytać dane z pliku");
            Console.WriteLine("Wcisnić Esc, aby wyjść z programu");
        }

        public static void LoadingDataCurrentCount(long currentRecord, long allRecords)
        {
            Console.Write("\rTrwa wczytywanie danych, wczytano " + currentRecord + " z " + allRecords + " rekordów");
        }

        public static void ResetDataMessage(long rowsCount)
        {
            Console.WriteLine("\nDane zostały ponownie wczytane z pliku, liczba rekordów: " + rowsCount + "\n");
        }

        public static void ShowDataMessage(long rowsCount)
        {
            Console.WriteLine("\rLiczba rekordów: " + rowsCount);
        }

        public static bool FilterDataMessage(long rowsCount)
        {
            Console.WriteLine("\nDane zostały przefiltrowane, liczba rekordów: " + rowsCount + "\n");
            Console.WriteLine("Wciśnij \"T\" aby wyświetlić dane");
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.T)
                return true;
            return false;
        }

        public static void ShowListRecords(List<CallRecord> data)
        {
            int i = 0;
            foreach (var row in data)
            {
                i++;
                Console.WriteLine();
                Console.Write(row.CallId + "   ");
                Console.Write(row.PhoneNumber + "   ");
                Console.Write(row.CallLine + "   ");
                Console.Write(row.DestPhoneNumber + "   ");
                Console.Write(row.CallStart + "   ");
                Console.Write(row.CallEnd + "   ");
                Console.Write(row.CallType + "   ");
                Console.Write(row.CallCharge);
                Console.WriteLine();

                if (i % Console.WindowHeight - 2 == 0)
                {
                    int rowsLeft = data.Count - i;
                    Console.WriteLine("Enter, aby wczytać kolejne (pozostało " +  rowsLeft + ")");
                    ConsoleKeyInfo keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.Enter)
                        continue;
                    else
                        break;
                }
                
            }
        }

        public static void FilterMenuMessage()
        {
            Console.WriteLine("*** Rodzaje filtrów ***");
            Console.WriteLine("- Numer telefonu dzwoniącego, wpisz: dzwoniacy");
            Console.WriteLine("- Numer telefonu odbierającego, wpisz: odbierajacy");
            Console.WriteLine("- Data rozpoczęcia połączenia, wpisz: rozpoczecie");
            Console.WriteLine("- Data zakończenia połączenia, wpisz: zakonczenie");
            Console.WriteLine("- Rodzaj połączenia, wpisz: rodzaj");
            Console.WriteLine("- Opłata za połączenie, wpisz: oplata");
            Console.WriteLine("\nPo czym filtrujesz?");
        }

        public static void FilterInstructionMessage(string filterType)
        {
            switch (filterType)
            {
                case "DZWONIACY":
                    Console.WriteLine("Podaj numer dzwoniącego telefonu");
                    break;
                case "ODBIERAJACY":
                    Console.WriteLine("Podaj numer odbierającego telefonu");
                    break;
                case "ROZPOCZECIE":
                    Console.WriteLine("Podaj operator [< = >], datę [dd/mm/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. > 11/01/2014 18:00)");
                    break;
                case "ZAKONCZENIE":
                    Console.WriteLine("Podaj operator [< = >], datę [dd/mm/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. > 11/01/2014 18:00)");
                    break;
                case "RODZAJ":
                    Console.WriteLine("Podaj typ połączenia (National/Mobile/Local/Intl/PRS/Free)");
                    break;
                case "OPLATA":
                    Console.WriteLine("Podaj operator [< = >] + kwotę po spacji (np. > 500)");
                    break;
            }
        }

        public static void FilterErrorMessage()
        {
            Console.WriteLine("Niepoprawne polecenie, dane nie zostały przefiltrowane");
        }

        public static void SaveFilterMessage()
        {
            Console.WriteLine("Wciśnij S, aby zapisać przefitlrowane dane");
        }
        
        public static void ParseError(string errMessage)
        {
            Console.WriteLine(errMessage);
            Console.WriteLine("Kliknij 'T', aby kontynuować wykonanie programu ");
        }

        public static void ParseError(string line, long lineNumber, string parseMessage)
        {
            Console.WriteLine("Ta linia (" + lineNumber + ") nie zostanie dodana:");
            Console.WriteLine("\t" + line);
            Console.WriteLine("Ze względu na:");
            Console.WriteLine(parseMessage);
        }
    }
}
