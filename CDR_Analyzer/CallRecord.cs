using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR_Analyzer
{
    /// <summary>
    /// Wzorzec danych przechowywanych w bazie
    /// </summary>
    public class CallRecord
    {
        /// <summary> Id wpisu w bazie danych </summary>
        public MongoDB.Bson.ObjectId _id { get; set; }
        /// <summary> Id połączenia </summary>
        public int CallId { get; set; }
        /// <summary> Number dzwoniącego </summary>
        public string PhoneNumber { get; set; }
        /// <summary> Używana linia </summary>
        public int CallLine { get; set; }
        /// <summary> Numer odbierającego </summary>
        public string DestPhoneNumber { get; set; }
        /// <summary> Data i godzina rozpoczęcia połączenia </summary>
        public DateTime CallStart { get; set; }
        /// <summary> Data i godzina zakończenia połączenia </summary>
        public DateTime CallEnd { get; set; }
        /// <summary> Rodzaj połączenia </summary>
        public string CallType { get; set; }
        /// <summary> Opłata za połączenie </summary>
        public float CallCharge { get; set; }
    }
}
