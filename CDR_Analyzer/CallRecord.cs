using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    public class CallRecord
    {
        public MongoDB.Bson.ObjectId _id { get; set; }
        public int CallId { get; set; }
        public string PhoneNumber { get; set; }
        public int CallLine { get; set; }
        public string DestPhoneNumber { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public string CallType { get; set; }
        public float CallCharge { get; set; }
    }
}
