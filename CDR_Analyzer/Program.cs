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
            /*var e = parserController.SavedDataRows.GroupBy(c => c.CallRange).Select(g => g.First()).ToList();
            foreach (var g in e)
            {
                Debug.WriteLine(g.CallRange.ToString());
            }*/
            
            Console.WriteLine("*** Witaj w programie analizującym pliki CDR ***");

            var request = parserController.SavedDataRows.Where(x => x.CallType == "National").ToList();
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
