using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    /// <summary>
    /// Statyczna klasa zawierająca metody służące do wyświetlania informacji i komend dla użytkownika
    /// </summary>
    public static class MessageController
    {
        /// <summary>
        /// Wiadomość powitalna służąca do wybrania na czym pracował będzie program
        /// </summary>
        /// <returns>True w przypadku wybrania bazy danych, false w przypadku wybrania list</returns>
        public static bool HelloMessage()
        {
            Console.WriteLine("*** Witaj w programie analizującym pliki CDR ***");
            Console.WriteLine("Domyślnie program do przechowywania danych wykorzystuje bazę MongoDb");
            Console.WriteLine("Wciśnij \"L\", aby przełączyć się na korzystanie z listy");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.L)
            {
                Console.WriteLine("\rWybrałeś operowanie na listach");
                return false;
            }
            Console.WriteLine("\rWybrałeś operowanie na bazie danych");
            return true;
        }

        /// <summary>
        /// Umożliwia wczytanie danych do bazy w przypadku gdy jest pusta
        /// </summary>
        /// <returns>True w przypadku naciśnięcia poprawnego klawisza, inaczej false</returns>
        public static bool StartLoadData()
        {
            Console.WriteLine("Baza jest pusta, wciśnij \"T\", aby wczytać dane z domyślnego pliku");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.T)
                return true;
            return false;
        }

        /// <summary>
        /// Wyświetla menu główne programu
        /// </summary>
        public static void MainMenuMessage()
        {
            Console.WriteLine("\nCo chcesz teraz zrobić?");
            Console.WriteLine("Wciśnij \"F\", aby przefitrować dane");
            Console.WriteLine("Wciśnij \"W\", aby wyświetlić obecne informacje aktualnych danych i ich źródle");
            Console.WriteLine("Wciśnij \"R\", aby ponownie wczytać dane z pliku");
            Console.WriteLine("WcisniJ \"Esc\", aby wyjść z programu");
        }

        /// <summary>
        /// Wyświetla informację o postępie wczytywania danych
        /// </summary>
        /// <param name="currentRecord">Obecnie wczytywany rekord</param>
        /// <param name="allRecords">Liczba wszystkich rekordów</param>
        public static void LoadingDataCurrentCount(long currentRecord, long allRecords)
        {
            Console.Write("\rTrwa wczytywanie danych, wczytano " + currentRecord + " z " + allRecords + " rekordów");
        }

        /// <summary>
        /// Wyświetla informację o czasie potrzebnym do wczytania danych
        /// </summary>
        /// <param name="loadingTime">Czas wczytania danych</param>
        public static void LoadingDataTime(TimeSpan loadingTime)
        {
            Console.WriteLine("\nWczytanie danych zajęło " + FormatRequestTime(loadingTime));
        }

        /// <summary>
        /// Wyświetla informację podsumowującą ponowne wczytanie danych
        /// </summary>
        /// <param name="rowsCount">Liczcba wczytanych rekordów</param>
        public static void ResetDataMessage(long rowsCount)
        {
            Console.WriteLine("\nDane zostały ponownie wczytane z pliku, liczba rekordów: " + rowsCount + "\n");
        }

        /// <summary>
        /// Pokazuje informacje o aktualnie używanych danych, umożliwia przełączenie się na bazę danych/listę
        /// </summary>
        /// <param name="rowsCount">Liczba rekordów</param>
        public static void ShowDataMessage(long rowsCount)
        {
            if (DB.useDb)
                Console.Write("\rAktualnie pracujesz na bazie danych, ");
            else
                Console.Write("\rAktualnie pracujesz na listach, ");
            Console.WriteLine("liczba rekordów: " + rowsCount);

            Console.WriteLine("Wciśnij \"S\" w celu przełączenia źródła danych lub dowolny przycisk, aby pozostać przy aktualnym");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.S)
            {
                DB.useDb = !DB.useDb;
                Console.Write("\rPrzełączyłeś się na ");
                if (DB.useDb)
                    Console.Write("bazę danych");
                else
                    Console.Write("listę\n");
            }
        }

        /// <summary>
        /// Wyświetla informacje o przefiltrowaniu danych
        /// </summary>
        /// <param name="rowsCount">Liczba rekordów po przefiltrowaniu</param>
        /// <param name="queryTime">Czas wykonania zapytania</param>
        /// <param name="correctRequest">Sprawdza czy użytkownik wykonał poprawne zapytanie</param>
        /// <returns>True jeśli użytkownik chce wyświetlić dane, inaczej false</returns>
        public static bool FilterDataMessage(long rowsCount, TimeSpan queryTime, bool correctRequest)
        {
            if (correctRequest)
            {
                Console.WriteLine("\nDane zostały przefiltrowane (w czasie " + FormatRequestTime(queryTime) + "), liczba rekordów: " + rowsCount + "\n");
                if (rowsCount != 0)
                {
                    Console.WriteLine("Wciśnij \"T\" aby wyświetlić dane");
                    ConsoleKeyInfo keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.T)
                        return true;
                }
            }
            else
            {
                Console.WriteLine("\rNieprawidłowe zapytanie");
            }
            return false;
        }

        /// <summary>
        /// Wyświetla dane strona po stronie
        /// </summary>
        /// <param name="data">Dane</param>
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
                    Console.WriteLine("\"Enter\", aby wczytać kolejne (pozostało " +  rowsLeft + ")");
                    ConsoleKeyInfo keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.Enter)
                        continue;
                    else
                        break;
                }
                
            }
        }

        /// <summary>
        /// Wyświetla listę dostępnych filtrów
        /// </summary>
        public static void FilterMenuMessage()
        {
            Console.WriteLine("\r*** Rodzaje filtrów ***");
            Console.WriteLine("\"1\" Numer telefonu dzwoniącego");
            Console.WriteLine("\"2\" Numer telefonu odbierającego");
            Console.WriteLine("\"3\" Data rozpoczęcia połączenia");
            Console.WriteLine("\"4\" Data zakończenia połączenia");
            Console.WriteLine("\"5\" Rodzaj połączenia");
            Console.WriteLine("\"6\" Opłata za połączenie");
            Console.WriteLine("\nPo czym filtrujesz?");
        }

        /// <summary>
        /// Wyświetla szczegółowe instrukcje jak używać filtrów
        /// </summary>
        /// <param name="filterType">Wybrany typ filtru</param>
        public static void FilterInstructionMessage(string filterType)
        {
            switch (filterType)
            {
                case "DZWONIACY":
                    Console.WriteLine("\rPodaj numer dzwoniącego telefonu");
                    break;
                case "ODBIERAJACY":
                    Console.WriteLine("\rPodaj numer odbierającego telefonu");
                    break;
                case "ROZPOCZECIE":
                    Console.WriteLine("\rPodaj operator [< = >], datę [dd/MM/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. > 11/01/2017 18:00)");
                    break;
                case "ZAKONCZENIE":
                    Console.WriteLine("\rPodaj operator [< = >], datę [dd/MM/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. < 14/02/2017 16:00)");
                    break;
                case "RODZAJ":
                    Console.WriteLine("\rPodaj typ połączenia [1] National | [2] Mobile | [3] Local | [4] Intl | [5] PRS | [6] Free");
                    break;
                case "OPLATA":
                    Console.WriteLine("\rPodaj operator [< = >] + kwotę po spacji (np. > 500)");
                    break;
            }
        }

        /// <summary>
        /// Wyświetla wiadomość o błędu podczas filtrowania danych
        /// </summary>
        public static void FilterErrorMessage()
        {
            Console.WriteLine("Niepoprawne polecenie, dane nie zostały przefiltrowane");
        }

        /// <summary>
        /// Wyświetla wiadomość dla użytkownika dotyczącą oczekiwania na zapytanie
        /// </summary>
        public static void WaitForQuery()
        {
            Console.WriteLine("\rTrwa wykonywanie zapytania, proszę czekać...");
        }

        /// <summary>
        /// Wyświetla informację o możliwości zapisania przefiltrowanych danych do pliku
        /// </summary>
        public static void SaveFilterMessage()
        {
            Console.WriteLine("\rWciśnij \"S\", aby zapisać przefiltrowane dane do pliku");
        }
        
        /// <summary>
        /// Wyświetla informację o możliwości dalszego działania na przefiltrowanych danych
        /// </summary>
        public static void KeepFilteredData()
        {
            Console.WriteLine("\rWciśnij \"T\", aby nadal pracować na przefiltrowanych danych?");
        }

        /// <summary>
        /// Wyświetla informację o błędach podczas parsowania
        /// </summary>
        /// <param name="errMessage">Typ błędu</param>
        public static void ParseError(string errMessage)
        {
            Console.WriteLine(errMessage);
            Console.WriteLine("Wciśnij \"T\", aby kontynuować wykonanie programu ");
        }

        /// <summary>
        /// Wyświetla informację o błędach w parsowaniu konkretnej linii
        /// </summary>
        /// <param name="line">Parsowana linia</param>
        /// <param name="lineNumber">Numer parsowanej linii</param>
        /// <param name="parseMessage">Wykryty błąd</param>
        public static void ParseError(string line, long lineNumber, string parseMessage)
        {
            Console.WriteLine("Ten rekord (" + lineNumber + ") nie zostanie dodany:");
            Console.WriteLine("\t" + line);
            Console.WriteLine("Ze względu na:");
            Console.WriteLine(parseMessage);
        }

        /// <summary>
        /// Formatuje wyświetlanie czasu zapytania, w zależności od jego długości
        /// </summary>
        /// <param name="queryTime">Czas zapytania</param>
        /// <returns>String z opisem czasu zapytania</returns>
        private static string FormatRequestTime(TimeSpan queryTime)
        {
            if (queryTime.TotalMilliseconds < 1000)
                return queryTime.Milliseconds + "ms";
            else
                return queryTime.TotalSeconds.ToString(".##") + "s";
        }
    }
}
