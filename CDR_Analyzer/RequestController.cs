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
        public List<ParserController.DataRow> FilterRequests(List<ParserController.DataRow> data, MessageController messageController)
        {
            messageController.FilterMenuMessage();

            string requestType = Console.ReadLine();
            var filteredData = SingleRequest(requestType, data, messageController);

            messageController.FilterDataMessage(filteredData.Count);

            messageController.SaveFilterMessage();
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.S)
                return filteredData;
            else
                return data;
        }

        List<ParserController.DataRow> SingleRequest(string requestType, List<ParserController.DataRow> data, MessageController messageController)
        {
            string requestMsg = "";
            requestType = requestType.ToUpper();
            messageController.FilterInstructionMessage(requestType);
            switch (requestType)
            {               
                case "DZWONIACY":
                    requestMsg = Console.ReadLine();
                    return data.Where(x => x.PhoneNumber == requestMsg).ToList();
                case "ODBIERAJACY":
                    requestMsg = Console.ReadLine();
                    return data.Where(x => x.DestPhoneNumber == requestMsg).ToList();
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
                    return messageController.FilterErrorMessage(data);
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
                    return messageController.FilterErrorMessage(data);
                case "RODZAJ":
                    requestMsg = Console.ReadLine();                   
                    return data.Where(x => x.CallType == requestMsg).ToList();
                case "OPLATA":
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
                    return messageController.FilterErrorMessage(data);
                default:
                    return messageController.FilterErrorMessage(data);
            }
        }
    }
}
