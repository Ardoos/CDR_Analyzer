using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CDR_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserController = new ParserController();
            var requestController = new RequestController();

            parserController.SetFilePath("..\\..\\output.txt");
            MessageController.HelloMessage();

            if (DB.CallRecords.Count(new BsonDocument()) == 0)
                if (MessageController.StartLoadData())
                    parserController.Parse();
         
            while (true)
            {
                MessageController.MainMenuMessage();
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.F)
                {
                    //Filtruj
                    requestController.FilterRequests();
                }
 
                if (keyPressed.Key == ConsoleKey.R)
                {
                    //Wczytaj dane ponownie
                    DB.CallRecords.Database.DropCollection("CallRecords");
                    parserController.Parse();
                }

                if (keyPressed.Key == ConsoleKey.W)
                {
                    //Pokaż informacje o danych
                    MessageController.ShowDataMessage(DB.CallRecords.Count(new BsonDocument()));
                }
                
                if(keyPressed.Key == ConsoleKey.Escape)
                {
                    //Wyjdź
                    Environment.Exit(0);
                }
            }
            /*var e = parserController.SavedDataRows.GroupBy(c => c.CallType).Select(g => g.First()).ToList();
            foreach (var g in e)
            {
                Debug.WriteLine(g.CallType.ToString());
            }*/
        }
    }
}
