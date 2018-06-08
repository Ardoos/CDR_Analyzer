using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CDR_Analyzer
{
    static public class DB
    {
        static public bool useDb = true;
        static private MongoClient client = new MongoClient("mongodb://localhost:27017");
        static public IMongoDatabase Database { get; private set; } = client.GetDatabase("CDR_Data");
        static public IMongoCollection<CallRecord> CallRecords { get; set; } =
            Database.GetCollection<CallRecord>("CallRecords");

    }
}
