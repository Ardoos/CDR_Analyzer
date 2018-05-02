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
            parserController.Parse();

            var requestController = new RequestController();
            
            var e = parserController.SavedDataRows.GroupBy(c => c.CallType).Select(g => g.First()).ToList();
            foreach (var g in e)
            {
                Debug.WriteLine(g.CallType.ToString());
            }
            
            Console.WriteLine("*** Witaj w programie analizującym pliki CDR ***");          

            var request = requestController.BasicRequests(parserController.SavedDataRows);

            Console.WriteLine("Liczba zwróconych wyników: " + request.ToList().Count + ", wciśnij \"T\" aby je wyświetlić");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.T)
            {       
                foreach (var result in request)
                {
                    Console.WriteLine();
                    Console.Write(result.CallId + "   ");
                    Console.Write(result.PhoneNumber + "   ");
                    Console.Write(result.CallLine + "   ");
                    Console.Write(result.DestPhoneNumber + "   ");
                    Console.Write(result.CallStart + "   ");
                    Console.Write(result.CallEnd + "   ");
                    Console.Write(result.CallType + "   ");
                    Console.Write(result.CallCharge);
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}
