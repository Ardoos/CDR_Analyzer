using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CDR_Analyzer
{
    public class RequestController
    {      
        public void FilterRequests()
        {
            MessageController.FilterMenuMessage();

            string requestType = Console.ReadLine();
            var filteredData = SingleRequest(requestType);

            if (MessageController.FilterDataMessage(filteredData.Count))
                MessageController.ShowListRecords(filteredData);              
        }

        List<CallRecord> SingleRequest(string requestType)
        {
            string requestMsg = "";
            requestType = requestType.ToUpper();
            MessageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {               
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    return DB.CallRecords.Find(t => t.PhoneNumber == requestMsg).ToList();
                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
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
                                    return DB.CallRecords.Find(t => t.CallStart.Date < startDate).ToList();
                                else if (startDateLine[0] == "=")
                                    return DB.CallRecords.Find(t => t.CallStart.Date == startDate).ToList();
                                else if (startDateLine[0] == ">")
                                    return DB.CallRecords.Find(t => t.CallStart.Date > startDate).ToList();
                            }
                            else if(startDateLine.Count() == 3)
                            {
                                if(DateTime.TryParse(startDateLine[2], out DateTime startTime))
                                {
                                    startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
                                    if (startDateLine[0] == "<")
                                        return DB.CallRecords.Find(t => t.CallStart < startDate).ToList();
                                    else if (startDateLine[0] == "=")
                                        return DB.CallRecords.Find(t => t.CallStart == startDate).ToList();
                                    else if (startDateLine[0] == ">")
                                        return DB.CallRecords.Find(t => t.CallStart > startDate).ToList();
                                }
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    return null;
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
                                    return DB.CallRecords.Find(t => t.CallEnd.Date < endDate).ToList();
                                else if (endDateLine[0] == "=")
                                    return DB.CallRecords.Find(t => t.CallEnd.Date == endDate).ToList();
                                else if (endDateLine[0] == ">")
                                    return DB.CallRecords.Find(t => t.CallEnd.Date > endDate).ToList();
                            }
                            else if (endDateLine.Count() == 3)
                            {
                                if (DateTime.TryParse(endDateLine[2], out DateTime endTime))
                                {
                                    endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);
                                    if (endDateLine[0] == "<")
                                        return DB.CallRecords.Find(t => t.CallEnd < endDate).ToList();
                                    else if (endDateLine[0] == "=")
                                        return DB.CallRecords.Find(t => t.CallEnd == endDate).ToList();
                                    else if (endDateLine[0] == ">")
                                        return DB.CallRecords.Find(t => t.CallEnd > endDate).ToList();
                                }
                            }
                        }
                    }
                    MessageController.FilterErrorMessage();
                    return null;
                case "RODZAJ":
                    requestMsg = Console.ReadLine();
                    return DB.CallRecords.Find(t => t.CallType == requestMsg).ToList();
                case "OPLATA":
                    requestMsg = Console.ReadLine();
                    string[] chargeLine = requestMsg.Split(' ');
                    if (chargeLine.Count() == 2)
                    {
                        if (float.TryParse(chargeLine[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float chargeAmount))
                        {
                            if (chargeLine[0] == "<")
                                return DB.CallRecords.Find(t => t.CallCharge < chargeAmount).ToList();
                            else if (chargeLine[0] == "=")
                                return DB.CallRecords.Find(t => t.CallCharge == chargeAmount).ToList();
                            else if (chargeLine[0] == ">")
                                return DB.CallRecords.Find(t => t.CallCharge > chargeAmount).ToList();
                        }
                    }
                    MessageController.FilterErrorMessage();
                    return null;
                default:
                    return null;
            }
        }
    }
}
