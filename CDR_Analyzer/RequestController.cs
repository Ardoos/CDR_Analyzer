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
    public class RequestController
    {
        public List<CallRecord> FilterRequests()
        {
            MessageController.FilterMenuMessage();

            ConsoleKeyInfo request = Console.ReadKey();
            Stopwatch stopWatch = new Stopwatch();

            var filteredData = SingleRequest(GetRequestType(request.Key), stopWatch);
            stopWatch.Stop();
            if (MessageController.FilterDataMessage(filteredData.Count, stopWatch.Elapsed))
                MessageController.ShowListRecords(filteredData);

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

            return new List<CallRecord>();
        }

        List<CallRecord> SingleRequest(string requestType, Stopwatch stopWatch)
        {
            string requestMsg = "";
            requestType = requestType.ToUpper();
            MessageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {               
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    stopWatch.Start();
                    return DB.CallRecords.Find(t => t.PhoneNumber == requestMsg).ToList();
                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
                    stopWatch.Start();
                    return DB.CallRecords.Find(t => t.DestPhoneNumber == requestMsg).ToList();
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
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart.Date < startDate).ToList();
                                }
                                else if (startDateLine[0] == "=")
                                {
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart.Date == startDate).ToList();
                                }
                                else if (startDateLine[0] == ">")
                                {
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallStart.Date > startDate).ToList();
                                }
                            }
                            else if(startDateLine.Count() == 3)
                            {
                                if(DateTime.TryParse(startDateLine[2], out DateTime startTime))
                                {
                                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
                                    if (startDateLine[0] == "<")
                                    {
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallStart < startDate).ToList();
                                    }
                                    else if (startDateLine[0] == "=")
                                    {
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallStart == startDate).ToList();
                                    }
                                    else if (startDateLine[0] == ">")
                                    {
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
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallEnd.Date < endDate).ToList();
                                }
                                else if (endDateLine[0] == "=")
                                {
                                    stopWatch.Start();
                                    return DB.CallRecords.Find(t => t.CallEnd.Date == endDate).ToList();
                                }
                                else if (endDateLine[0] == ">")
                                {
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
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallEnd < endDate).ToList();
                                    }
                                    else if (endDateLine[0] == "=")
                                    {
                                        stopWatch.Start();
                                        return DB.CallRecords.Find(t => t.CallEnd == endDate).ToList();
                                    }
                                    else if (endDateLine[0] == ">")
                                    {
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
                    stopWatch.Start();
                    return DB.CallRecords.Find(t => t.CallType == GetCallType(request.Key)).ToList();
                case "OPLATA":
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                            {
                                stopWatch.Start();
                                return DB.CallRecords.Find(t => t.CallCharge < chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == "=")
                            {
                                stopWatch.Start();
                                return DB.CallRecords.Find(t => t.CallCharge == chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == ">")
                            {
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
                    return new List<CallRecord>();
            }
        }

        public List<CallRecord> FilterRequests(List<CallRecord> data)
        {
            MessageController.FilterMenuMessage();

            ConsoleKeyInfo request = Console.ReadKey();
            Stopwatch stopWatch = new Stopwatch();

            var filteredData = SingleRequest(GetRequestType(request.Key), stopWatch, data);
            stopWatch.Stop();
            if (MessageController.FilterDataMessage(filteredData.Count, stopWatch.Elapsed))
                MessageController.ShowListRecords(filteredData);

            MessageController.SaveFilterMessage();
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.S)
                SaveData.SaveToFile(filteredData);

            MessageController.KeepFilteredData();
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.T)
                return filteredData;
            else
                return data;
        }

        List<CallRecord> SingleRequest(string requestType, Stopwatch stopWatch, List<CallRecord> data)
        {
            string requestMsg = "";
            requestType = requestType.ToUpper();
            MessageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    stopWatch.Start();
                    return data.Where(x => x.PhoneNumber == requestMsg).ToList();
                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
                    stopWatch.Start();
                    return data.Where(x => x.DestPhoneNumber == requestMsg).ToList();
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
                                    stopWatch.Start();
                                    return data.Where(x => x.CallStart.Date < startDate).ToList();
                                }
                                else if (startDateLine[0] == "=")
                                {
                                    stopWatch.Start();
                                    return data.Where(x => x.CallStart.Date == startDate).ToList();
                                }
                                else if (startDateLine[0] == ">")
                                {
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
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart < startDate).ToList();
                                    }
                                    else if (startDateLine[0] == "=")
                                    {
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart == startDate).ToList();
                                    }
                                    else if (startDateLine[0] == ">")
                                    {
                                        stopWatch.Start();
                                        return data.Where(x => x.CallStart > startDate).ToList();
                                    }
                                }
                            }
                        }
                    }
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
                                    stopWatch.Start();
                                    return data.Where(x => x.CallEnd.Date < endDate).ToList();
                                }
                                else if (endDateLine[0] == "=")
                                {
                                    stopWatch.Start();
                                    return data.Where(x => x.CallEnd.Date == endDate).ToList();
                                }
                                else if (endDateLine[0] == ">")
                                {
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
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd < endDate).ToList();
                                    }
                                    else if (endDateLine[0] == "=")
                                    {
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd == endDate).ToList();
                                    }
                                    else if (endDateLine[0] == ">")
                                    {
                                        stopWatch.Start();
                                        return data.Where(x => x.CallEnd > endDate).ToList();
                                    }
                                }
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return data;
                case "RODZAJ":
                    ConsoleKeyInfo request = Console.ReadKey();
                    stopWatch.Start();
                    return data.Where(x => x.CallType == GetCallType(request.Key)).ToList();
                case "OPLATA":
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                            {
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge < chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == "=")
                            {
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge == chargeAmount).ToList();
                            }
                            else if (chargeLine[0] == ">")
                            {
                                stopWatch.Start();
                                return data.Where(x => x.CallCharge > chargeAmount).ToList();
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    stopWatch.Start();
                    return data;
                default:
                    stopWatch.Start();
                    return data;
            }
        }

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
    }
}
