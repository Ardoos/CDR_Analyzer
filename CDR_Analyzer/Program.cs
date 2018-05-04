using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace CDR_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserController = new ParserController();
            var requestController = new RequestController();
            var messageController = new MessageController();

            parserController.Parse();

            messageController.HelloMessage();

            while (true)
            {
                messageController.MainMenuMessage();
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.F)
                {
                    var request = requestController.FilterRequests(parserController.SavedDataRows, messageController);
                    parserController.SavedDataRows = request;
                    parserController.SavedDataRows.TrimExcess();
                    GC.Collect();
                }
 
                if (keyPressed.Key == ConsoleKey.R)
                {
                    parserController.Parse();
                    messageController.ResetDataMessage(parserController.SavedDataRows.Count);
                }

                if (keyPressed.Key == ConsoleKey.W)
                {
                    messageController.ShowDataMessage(parserController.SavedDataRows.Count);
                    keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.T)
                    {
                        messageController.ShowListRecords(parserController.SavedDataRows);
                    }
                }
                
                if(keyPressed.Key == ConsoleKey.Escape)
                {
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
