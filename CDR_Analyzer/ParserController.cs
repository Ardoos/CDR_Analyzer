using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    public class ParserController
    {
        public class DataRow
        {
            public int CallId { get; set; }
            public string PhoneNumber { get; set; }
            public int CallLine { get; set; }
            public string DestPhoneNumber { get; set; }
            public DateTime CallStart { get; set; }
            public DateTime CallEnd { get; set; }
            public string CallType { get; set; }
            public float CallCharge { get; set; }

        }
        public List<DataRow> SavedDataRows { get; set; } = new List<DataRow>();
        string filePath;
        private string errMessage;
        private string parseMessage;
        private string SavedValue { get; set; }
        private int dataInLineNumber = 10;
  
        public bool Parse(MessageController messageController)
        {
            SavedDataRows = new List<DataRow>();

            try
            {
                string[] lines;
                if (!File.Exists(this.filePath))
                    return ErrorReturn("Plik nie istnieje", messageController);

                lines = System.IO.File.ReadAllLines(this.filePath, Encoding.GetEncoding(949));

                if (lines.Length < 1)
                    throw new Exception("Plik jest pusty");

                int i = 1;
                foreach (string line in lines)
                {
                    bool parseError = false;
                    parseMessage = "";
                    string[] valueLineParts = line.Split(',');

                    if (valueLineParts.Length != dataInLineNumber)
                    {
                        messageController.ParseError(line, i, "\tNieprawidłowa ilość danych w linii");
                        continue;
                    }

                    if (!Int32.TryParse(valueLineParts[0], out int callId))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowe id\n";
                    }
                    if (!IsNumeric(valueLineParts[1]))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy numer dzwoniącego\n";
                    }
                    if (!Int32.TryParse(valueLineParts[2], out int callLine))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy numer linii\n";
                    }
                    if (!IsNumeric(valueLineParts[3]))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy numer odbierającego\n";
                    }
                    if (!DateTime.TryParse(valueLineParts[4], out DateTime dateStart))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowa data rozpoczęcia rozmowy\n";
                    }
                    if (DateTime.TryParse(valueLineParts[6], out DateTime timeStart))
                    {
                        dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, timeStart.Hour, timeStart.Minute, timeStart.Second);
                    }
                    else
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy czas rozpoczęcia rozmowy\n";
                    }
                    if (!DateTime.TryParse(valueLineParts[5], out DateTime dateEnd))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowa data zakończenia rozmowy\n";
                    }
                    if (DateTime.TryParse(valueLineParts[7], out DateTime timeEnd))
                    {
                        dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, timeEnd.Hour, timeEnd.Minute, timeEnd.Second);
                    }
                    else
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy czas zakończenia rozmowy\n";
                    }

                    if (valueLineParts[8] == "National" || valueLineParts[8] == "Mobile" || valueLineParts[8] == "Local" 
                        || valueLineParts[8] == "Intl" || valueLineParts[8] == "PRS" || valueLineParts[8] == "Free")
                    {} else
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowy typ połączenia\n";
                    }
                    if (!float.TryParse(valueLineParts[9], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float callCharge))//float.TryParse(valueLineParts[9], out float callCharge))
                    {
                        parseError = true;
                        parseMessage += "\tNieprawidłowa opłata za połączenie\n";
                    }

                    if (!parseError)
                    {
                        var newRecord = new DataRow()
                        {
                            CallId = callId,
                            PhoneNumber = valueLineParts[1],
                            CallLine = callLine,
                            DestPhoneNumber = valueLineParts[3],
                            CallStart = dateStart,
                            CallEnd = dateEnd,
                            CallType = valueLineParts[8],
                            CallCharge = callCharge
                        };
                        SavedDataRows.Add(newRecord);
                    }
                    else
                    {
                        messageController.ParseError(line, i, parseMessage);
                    }
                    i++;
                }

                return true;
            }
            catch (Exception e)
            {
                return ErrorReturn("Wyjątek:" + e.Message, messageController);
            }
        }

        private bool ErrorReturn(string message, MessageController messageController)
        {
            messageController.ParseError(message);
            errMessage += message + " ";

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.T)
                return false;
            else
                Environment.Exit(0);
            return true;
        }

        public void SetFilePath(string path)
        {
            filePath = path;
            errMessage = "";
        }

        public static bool IsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
