using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CDR_Analyzer
{
    /// <summary>
    /// Klasa obsługująca zapytania dotyczące filtrowania danych
    /// </summary>
    public class RequestController
    {
        /// <summary>
        /// Metoda wywołująca filtrowanie danych dla bazy danych MongoDB
        /// </summary>
        /// <returns>Listę przefiltrowanych danych</returns>
        public List<CallRecord> FilterRequests()
        {
            MessageController.FilterMenuMessage();

            ConsoleKeyInfo request = Console.ReadKey();
            Stopwatch stopWatch = new Stopwatch();
            bool correctRequest = true;
            var filteredData = SingleRequest(GetRequestType(request.Key), stopWatch, out correctRequest);
            stopWatch.Stop();
            if (MessageController.FilterDataMessage(filteredData.Count, stopWatch.Elapsed, correctRequest))
                MessageController.ShowListRecords(filteredData);

            if(filteredData.Count > 0)
            {
                MessageController.SaveFilterMessage();
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.S)
                    SaveData.SaveToFile(filteredData);

                MessageController.KeepFilteredData();
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.T)
                {
                    DB.useDb = false;
                    return filteredData;
                }
            }

            return new List<CallRecord>();
        }

        /// <summary>
        /// Obsługuje pojedyncze zapytanie do bazy danych
        /// </summary>
        /// <param name="requestType">Typ zapytania</param>
        /// <param name="stopWatch">Stoper odmierzający czas zapytania</param>
        /// <param name="correctRequest">Określa czy zapytanie było prawidłowe</param>
        /// <returns>Listę przefiltrowanych danych</returns>
        List<CallRecord> SingleRequest(string requestType, Stopwatch stopWatch, out bool correctRequest)
        {
            correctRequest = true;
            string requestMsg = "";
            requestType = requestType.ToUpper();
            MessageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {               
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if(IsPhoneNumber(requestMsg))
                        return DB.CallRecords.Find(t => t.PhoneNumber == requestMsg).ToList();
                    return new List<CallRecord>();

                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if(IsPhoneNumber(requestMsg))
                        return DB.CallRecords.Find(t => t.DestPhoneNumber == requestMsg).ToList();
                    return new List<CallRecord>();
                case "ROZPOCZECIE":
                    requestMsg = Console.ReadLine();
                    string[] startDateLine = requestMsg.Split(' ');
                    if (startDateLine.Count() >= 2)
                    {
                        if(DateTime.TryParse(startDateLine[1], out DateTime startDate))
                        {
                            if(startDateLine.Count() == 2)
                            {
                                if (startDateLine[0] == "<")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart < startDate).ToList();
                                }
                                else if (startDateLine[0] == "=")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart == startDate).ToList();
                                }
                                else if (startDateLine[0] == ">")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart > startDate).ToList();
                                }
                            }
                            else if(startDateLine.Count() == 3)
                            {
                                if(DateTime.TryParse(startDateLine[2], out DateTime startTime))
                                {
                                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
                                    if (startDateLine[0] == "<")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallStart < startDate).ToList();
                                    }
                                    else if (startDateLine[0] == "=")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallStart == startDate).ToList();
                                    }
                                    else if (startDateLine[0] == ">")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallStart > startDate).ToList();
                                    }
                                }
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return new List<CallRecord>();
                case "ZAKONCZENIE":
                    requestMsg = Console.ReadLine();
                    string[] endDateLine = requestMsg.Split(' ');
                    if (endDateLine.Count() >= 2)
                    {
                        if (DateTime.TryParse(endDateLine[1], out DateTime endDate))
                        {
                            if (endDateLine.Count() == 2)
                            {
                                if (endDateLine[0] == "<")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallEnd.Date < endDate).ToList();
                                }
                                else if (endDateLine[0] == "=")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallEnd.Date == endDate).ToList();
                                }
                                else if (endDateLine[0] == ">")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallEnd.Date > endDate).ToList();
                                }
                            }
                            else if (endDateLine.Count() == 3)
                            {
                                if (DateTime.TryParse(endDateLine[2], out DateTime endTime))
                                {
                                    endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);
                                    if (endDateLine[0] == "<")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallEnd < endDate).ToList();
                                    }
                                    else if (endDateLine[0] == "=")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallEnd == endDate).ToList();
                                    }
                                    else if (endDateLine[0] == ">")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallEnd > endDate).ToList();
                                    }
                                }
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return new List<CallRecord>();
                case "RODZAJ":
                    ConsoleKeyInfo request = Console.ReadKey();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if(GetCallType(request.Key) != "")
                        return DB.CallRecords.Find(t => t.CallType == GetCallType(request.Key)).ToList();
                    return new List<CallRecord>();
                case "OPLATA":
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return DB.CallRecords.Find(t => t.CallCharge < chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == "=")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return DB.CallRecords.Find(t => t.CallCharge == chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == ">")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return DB.CallRecords.Find(t => t.CallCharge > chargeAmount).ToList();
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return new List<CallRecord>();
                default:
                    stopWatch.Start();
                    correctRequest = false;
                    return new List<CallRecord>();
            }
        }

        /// Metoda wywołująca filtrowanie danych dla listy
        /// </summary>
        /// <param name="data">Lista z danymi wejściowymi</param>
        /// <returns>Listę z przefiltrowanymi danymi</returns>
        public List<CallRecord> FilterRequests(List<CallRecord> data)
        {
            MessageController.FilterMenuMessage();

            ConsoleKeyInfo request = Console.ReadKey();
            Stopwatch stopWatch = new Stopwatch();
            bool correctRequest = true;

            var filteredData = SingleRequest(GetRequestType(request.Key), stopWatch, data, out correctRequest);
            stopWatch.Stop();
            if (MessageController.FilterDataMessage(filteredData.Count, stopWatch.Elapsed, correctRequest))
                MessageController.ShowListRecords(filteredData);

            if (filteredData.Count > 0)
            {
                MessageController.SaveFilterMessage();
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.S)
                    SaveData.SaveToFile(filteredData);
           
                MessageController.KeepFilteredData();
                keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.T)
                    return filteredData;
            }
            return data;
        }

        /// <summary>
        /// Obsługuje pojedyncze zapytanie do listy
        /// </summary>
        /// <param name="requestType">Typ zapytania</param>
        /// <param name="stopWatch">Stoper odmierzający czas zapytania</param>
        /// <param name="data">Lista z danymi wejściowymi</param>
        /// <param name="correctRequest">Określa czy zapytanie było prawidłowe</param>
        /// <returns>Listę przefiltrowanych danych</returns>
        List<CallRecord> SingleRequest(string requestType, Stopwatch stopWatch, List<CallRecord> data, out bool correctRequest)
        {
            correctRequest = true;
            string requestMsg = "";
            requestType = requestType.ToUpper();
            MessageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if(IsPhoneNumber(requestMsg))
                        return data.Where(x => x.PhoneNumber == requestMsg).ToList();
                    return data;
                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if (IsPhoneNumber(requestMsg))
                        return data.Where(x => x.DestPhoneNumber == requestMsg).ToList();
                    return data;
                case "ROZPOCZECIE":
                    requestMsg = Console.ReadLine();
                    string[] startDateLine = requestMsg.Split(' ');
                    if (startDateLine.Count() >= 2)
                    {
                        if (DateTime.TryParse(startDateLine[1], out DateTime startDate))
                        {
                            if (startDateLine.Count() == 2)
                            {
                                if (startDateLine[0] == "<")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallStart.Date < startDate).ToList();
                                }
                                else if (startDateLine[0] == "=")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallStart.Date == startDate).ToList();
                                }
                                else if (startDateLine[0] == ">")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallStart.Date > startDate).ToList();
                                }
                            }
                            else if (startDateLine.Count() == 3)
                            {
                                if (DateTime.TryParse(startDateLine[2], out DateTime startTime))
                                {
                                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
                                    if (startDateLine[0] == "<")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart < startDate).ToList();
                                    }
                                    else if (startDateLine[0] == "=")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart == startDate).ToList();
                                    }
                                    else if (startDateLine[0] == ">")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart > startDate).ToList();
                                    }
                                }
                            }
                        }
                    }
                    MessageController.WaitForQuery();
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return data;
                case "ZAKONCZENIE":
                    requestMsg = Console.ReadLine();
                    string[] endDateLine = requestMsg.Split(' ');
                    if (endDateLine.Count() >= 2)
                    {
                        if (DateTime.TryParse(endDateLine[1], out DateTime endDate))
                        {
                            if (endDateLine.Count() == 2)
                            {
                                if (endDateLine[0] == "<")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallEnd.Date < endDate).ToList();
                                }
                                else if (endDateLine[0] == "=")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallEnd.Date == endDate).ToList();
                                }
                                else if (endDateLine[0] == ">")
                                {
                                    MessageController.WaitForQuery();
                                    stopWatch.Start();
                                    return data.Where(x => x.CallEnd.Date > endDate).ToList();
                                }
                            }
                            else if (endDateLine.Count() == 3)
                            {
                                if (DateTime.TryParse(endDateLine[2], out DateTime endTime))
                                {
                                    endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);
                                    if (endDateLine[0] == "<")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd < endDate).ToList();
                                    }
                                    else if (endDateLine[0] == "=")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd == endDate).ToList();
                                    }
                                    else if (endDateLine[0] == ">")
                                    {
                                        MessageController.WaitForQuery();
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd > endDate).ToList();
                                    }
                                }
                            }
                        }
                    }
                    MessageController.WaitForQuery();
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return data;
                case "RODZAJ":
                    ConsoleKeyInfo request = Console.ReadKey();
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    if (GetCallType(request.Key) != "")
                        return data.Where(x => x.CallType == GetCallType(request.Key)).ToList();
                    return data;
                case "OPLATA":
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge < chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == "=")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge == chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == ">")
                            {
                                MessageController.WaitForQuery();
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge > chargeAmount).ToList();
                            }
                        }
                    }
                    MessageController.WaitForQuery();
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return data;
                default:
                    correctRequest = false;
                    MessageController.WaitForQuery();
                    stopWatch.Start();
                    return data;
            }
        }

        /// <summary>
        /// Metoda sprawdzająca jakie należy wykonać zapytanie
        /// </summary>
        /// <param name="request">Przycisk wciśnięty przez użytkownika</param>
        /// <returns>Typ zapytania</returns>
        private string GetRequestType(ConsoleKey request)
        {
            if (request == ConsoleKey.D1 || request == ConsoleKey.NumPad1)
                return "dzwoniacy";
            else if (request == ConsoleKey.D2 || request == ConsoleKey.NumPad2)
                return "odbierajacy";
            else if (request == ConsoleKey.D3 || request == ConsoleKey.NumPad3)
                return "rozpoczecie";
            else if (request == ConsoleKey.D4 || request == ConsoleKey.NumPad4)
                return "zakonczenie";
            else if (request == ConsoleKey.D5 || request == ConsoleKey.NumPad5)
                return "rodzaj";
            else if (request == ConsoleKey.D6 || request == ConsoleKey.NumPad6)
                return "oplata";
            return "";
        }

        /// <summary>
        /// Metoda sprawdzająca o jaki typ połączenia pyta użytkownik
        /// </summary>
        /// <param name="request">Przycisk wciśnięty przez użytkownika</param>
        /// <returns>Typ połączenia</returns>
        private string GetCallType(ConsoleKey request)
        {
            if (request == ConsoleKey.D1 || request == ConsoleKey.NumPad1)
                return "National";
            else if (request == ConsoleKey.D2 || request == ConsoleKey.NumPad2)
                return "Mobile";
            else if (request == ConsoleKey.D3 || request == ConsoleKey.NumPad3)
                return "Local";
            else if (request == ConsoleKey.D4 || request == ConsoleKey.NumPad4)
                return "Intl";
            else if (request == ConsoleKey.D5 || request == ConsoleKey.NumPad5)
                return "PRS";
            else if (request == ConsoleKey.D6 || request == ConsoleKey.NumPad6)
                return "Free";
            return "";
        }

        /// <summary>
        /// Metoda sprawdzająca czy podany numer telefonu jest prawidłowy
        /// </summary>
        /// <param name="s">Podany numer telefonu</param>
        /// <returns>True jeśli numer telefonu prawidłowy, inaczej false</returns>
        private bool IsPhoneNumber(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {

                    return false;
                }
            }
            if(s.Length == 11) // Numery telefonów w wygenerowanych danych są 11 cyfrowe
                return true;
            return false;
        }
    }
}
