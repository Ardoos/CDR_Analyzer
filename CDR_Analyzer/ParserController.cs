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
        string filePath = "D:/Adrian Dokumenty/Studia/III rok/VI semestr/Techniki multimedialne/Projekt/CDR Generator by Paul Kinlan/Git/output.txt";
        public string SavedValue { get; set; }
  
        public bool Parse()
        {
//            try
//            {
                string[] lines;
                //if (!File.Exists(this.filePath))
                //    return ErrorReturn("File does not exist.");

                lines = System.IO.File.ReadAllLines(this.filePath, Encoding.GetEncoding(949));

                /*if (lines.Length < 1)
                    throw new Exception("File is empty");

                if (!ParseFilePath())
                    return ErrorReturn("");

                if (!ParseHeaderLine(lines[0]))
                    return ErrorReturn("");

                if (!modelName.Equals(fileModelName) || !lotName.Equals(fileLotName))
                    return ErrorReturn("File name does not correspond with the model/lot number in the content.");*/

                //this.dataList.Clear();

                foreach (string line in lines)
                {
                    string[] valueLineParts = line.Split(',');
                    if (!Int32.TryParse(valueLineParts[0], out int callId))
                    {
                        callId = -1;
                    }
                    if (!Int32.TryParse(valueLineParts[2], out int callLine))
                    {
                        callLine = -1;
                    }
                    if (!DateTime.TryParse(valueLineParts[4], out DateTime dateStart))
                    {
                        dateStart = new DateTime();
                    }
                    if (DateTime.TryParse(valueLineParts[6], out DateTime timeStart))
                    {
                        dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, timeStart.Hour, timeStart.Minute, timeStart.Second);
                    }
                    if (!DateTime.TryParse(valueLineParts[5], out DateTime dateEnd))
                    {
                        dateEnd = new DateTime();
                    }
                    if (DateTime.TryParse(valueLineParts[7], out DateTime timeEnd))
                    {
                        dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, timeEnd.Hour, timeEnd.Minute, timeEnd.Second);
                    }
                    if (!float.TryParse(valueLineParts[9], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float callCharge))//float.TryParse(valueLineParts[9], out float callCharge))
                    {
                        callCharge = -1.0f;
                    }
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
                    /*if (valueLineParts.Length != POINT_COUNT + 7)
                    {
                        continue;
                    }*/

                    /*if (!ParseValueLine(line))
                    {
                        this.dataList.Clear();
                        return ErrorReturn("");
                    }*/
                }

                //if (this.dataList.Count == 0)
                 //   return ErrorReturn("No input data found.");

                return true;
            //}
/*            catch (Exception e)
            {
                //return ErrorReturn("exception:" + e.Message);
                return false;
            }*/
        }
    }
}
