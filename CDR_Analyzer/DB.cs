using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CDR_Analyzer
{
    /// <summary>
    /// Klasa przechowująca dane konfiguracyjne bazy MongoDB
    /// </summary>
    static public class DB
    {
        /// <summary> Przechowuje informacje czy program ma korzystać z bazy danych (true), czy listy (false) </summary>
        static public bool useDb = true; //asdasdsa
        /// <summary> Konfiguracja połączenia </summary>
        static private MongoClient client = new MongoClient("mongodb://localhost:27017");
        /// <summary> Przechowuje nazwę bazy danych </summary>
        static public IMongoDatabase Database { get; private set; } = client.GetDatabase("CDR_Data");
        /// <summary> Wskazuje odpowiednią kolekcję w bazie danych </summary>
        static public IMongoCollection<CallRecord> CallRecords { get; set; } =
            Database.GetCollection<CallRecord>("CallRecords");

    }
}
