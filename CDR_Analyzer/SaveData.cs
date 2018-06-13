using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CDR_Analyzer
{
    public static class SaveData
    {
        public static void SaveToFile(List<CallRecord> data)
        {
            List<string> lines = new List<string>();
            int i = 0;
            foreach (var x in data)
            {
                string line = i + "," + x.PhoneNumber + "," + x.CallLine + "," + x.DestPhoneNumber + "," + x.CallStart.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "," +
                    x.CallEnd.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "," + x.CallStart.TimeOfDay + "," + x.CallEnd.TimeOfDay + "," + x.CallType + "," + x.CallCharge.ToString(".0", CultureInfo.InvariantCulture);
                lines.Add(line);
            }
            System.IO.File.WriteAllLines("..\\..\\filtered.txt", lines);
        }
    }
}
