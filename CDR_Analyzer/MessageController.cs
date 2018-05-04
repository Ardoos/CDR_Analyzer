using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    public class MessageController
    {
        public void HelloMessage()
        {
            Console.WriteLine("*** Witaj w programie analizującym pliki CDR ***");
        }

        public void MainMenuMessage()
        {
            Console.WriteLine("\nCo chcesz teraz zrobić?");
            Console.WriteLine("Wciśnij F, aby przefitrować dane");
            Console.WriteLine("Wciśnij W, aby wyświetlić obecne informacje o aktualnych danych");
            Console.WriteLine("Wciśnij R, aby ponownie wczytać dane z pliku");
            Console.WriteLine("Wcisnić Esc, aby wyjść z programu");
        }

        public void ResetDataMessage(int rowsCount)
        {
            Console.WriteLine("\nDane zostały ponownie wczytane z pliku, liczba rekordów: " + rowsCount + "\n");
        }

        public void ShowDataMessage(int rowsCount)
        {
            Console.WriteLine("Liczba rekordów: " + rowsCount + ", wciśnij \"T\" aby je wyświetlić");
        }

        public void FilterDataMessage(int rowsCount)
        {
            Console.WriteLine("\nDane zostały przefiltrowane, liczba rekordów: " + rowsCount + "\n");
        }

        public void ShowListRecords(List<ParserController.DataRow> data)
        {
            foreach (var row in data)
            {
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
            }
        }

        public void FilterMenuMessage()
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

        public void FilterInstructionMessage(string filterType)
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

        public List<ParserController.DataRow> FilterErrorMessage(List<ParserController.DataRow> data)
        {
            Console.WriteLine("Niepoprawne polecenie, dane nie zostały przefiltrowane");
            return data;
        }

        public void SaveFilterMessage()
        {
            Console.WriteLine("Wciśnij S, aby zapisać przefitlrowane dane");
        }
    }
}
