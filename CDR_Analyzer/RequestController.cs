using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;


namespace CDR_Analyzer
{
    public class RequestController
    {
        public List<ParserController.DataRow> BasicRequests(List<ParserController.DataRow> data)
        {
            Console.WriteLine("*** Rodzaje filtrów ***");
            Console.WriteLine("- Numer telefonu dzwoniącego, wpisz: dzwoniacy");
            Console.WriteLine("- Numer telefonu odbierającego, wpisz: odbierajacy");
            Console.WriteLine("- Data rozpoczęcia połączenia, wpisz: rozpoczecie");
            Console.WriteLine("- Data zakończenia połączenia, wpisz: zakonczenie");
            Console.WriteLine("- Rodzaj połączenia, wpisz: rodzaj");
            Console.WriteLine("- Opłata za połączenie, wpisz: oplata");
            Console.WriteLine("\nPo czym filtrujesz?");
            string requestType = Console.ReadLine();
            var filteredData = SingleRequest(requestType, data);

            return filteredData;
        }

        List<ParserController.DataRow> SingleRequest(string requestType, List<ParserController.DataRow> data)
        {
            string requestMsg = "";
            requestType = requestType.ToUpper();
            switch (requestType)
            {
                case "DZWONIACY":
                    Console.WriteLine("Podaj numer dzwoniącego telefonu");
                    requestMsg = Console.ReadLine();
                    return data.Where(x => x.PhoneNumber == requestMsg).ToList();
                case "ODBIERAJACY":
                    Console.WriteLine("Podaj numer odbierającego telefonu");
                    requestMsg = Console.ReadLine();
                    return data.Where(x => x.DestPhoneNumber == requestMsg).ToList();
                case "ROZPOCZECIE":
                    Console.WriteLine("Podaj operator [< = >], datę [dd/mm/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. > 11/01/2014 18:00)");
                    requestMsg = Console.ReadLine();
                    string[] startDateLine = requestMsg.Split(' ');
                    if (startDateLine.Count() >= 2)
                    {
                        if(DateTime.TryParse(startDateLine[1], out DateTime startDate))
                        {
                            if(startDateLine.Count() == 2)
                            {
                                if (startDateLine[0] == "<")
                                    return data.Where(x => x.CallStart.Date < startDate).ToList();
                                else if (startDateLine[0] == "=")
                                    return data.Where(x => x.CallStart.Date == startDate).ToList();
                                else if (startDateLine[0] == ">")
                                    return data.Where(x => x.CallStart.Date > startDate).ToList();
                            }
                            else if(startDateLine.Count() == 3)
                            {
                                if(DateTime.TryParse(startDateLine[2], out DateTime startTime))
                                {
                                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
                                    if (startDateLine[0] == "<")
                                        return data.Where(x => x.CallStart < startDate).ToList();
                                    else if (startDateLine[0] == "=")
                                        return data.Where(x => x.CallStart == startDate).ToList();
                                    else if (startDateLine[0] == ">")
                                        return data.Where(x => x.CallStart > startDate).ToList();
                                }
                            }
                        }
                    }
                    return ErrorMsg(data);
                case "ZAKONCZENIE":
                    Console.WriteLine("Podaj operator [< = >], datę [dd/mm/yyyy], opcjonalnie czas [hh:mm:(ss)] (np. > 11/01/2014 18:00)");
                    requestMsg = Console.ReadLine();
                    string[] endDateLine = requestMsg.Split(' ');
                    if (endDateLine.Count() >= 2)
                    {
                        if (DateTime.TryParse(endDateLine[1], out DateTime endDate))
                        {
                            if (endDateLine.Count() == 2)
                            {
                                if (endDateLine[0] == "<")
                                    return data.Where(x => x.CallEnd.Date < endDate).ToList();
                                else if (endDateLine[0] == "=")
                                    return data.Where(x => x.CallEnd.Date == endDate).ToList();
                                else if (endDateLine[0] == ">")
                                    return data.Where(x => x.CallEnd.Date > endDate).ToList();
                            }
                            else if (endDateLine.Count() == 3)
                            {
                                if (DateTime.TryParse(endDateLine[2], out DateTime endTime))
                                {
                                    endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);
                                    if (endDateLine[0] == "<")
                                        return data.Where(x => x.CallEnd < endDate).ToList();
                                    else if (endDateLine[0] == "=")
                                        return data.Where(x => x.CallEnd == endDate).ToList();
                                    else if (endDateLine[0] == ">")
                                        return data.Where(x => x.CallEnd > endDate).ToList();
                                }
                            }
                        }
                    }
                    return ErrorMsg(data);
                case "RODZAJ":
                    Console.WriteLine("Podaj typ połączenia (National/Mobile/Local/Intl/PRS/Free)");
                    requestMsg = Console.ReadLine();                   
                    return data.Where(x => x.CallType == requestMsg).ToList();
                case "OPLATA":
                    Console.WriteLine("Podaj operator [< = >] + kwotę po spacji (np. > 500)");
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                                return data.Where(x => x.CallCharge < chargeAmount).ToList();
                            else if (chargeLine[0] == "=")
                                return data.Where(x => x.CallCharge == chargeAmount).ToList();
                            else if (chargeLine[0] == ">")
                                return data.Where(x => x.CallCharge > chargeAmount).ToList();
                        }
                    }
                    return ErrorMsg(data);
                default:
                    return ErrorMsg(data);
            }
        }

        List<ParserController.DataRow> ErrorMsg(List<ParserController.DataRow> data)
        {
            Console.WriteLine("Niepoprawne polecenie, dane nie zostały przefiltrowane");
            return data;
        }
    }
}
