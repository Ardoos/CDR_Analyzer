﻿using System;
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
    /// <summary>
    /// Główna klasa programu w której znajduje się metoda main
    /// </summary>
    class Program
    {
        /// <summary>
        /// Główna metoda programu, uruchamia się jako pierwsza
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var parserController = new ParserController();
            var requestController = new RequestController();

            parserController.SetFilePath("..\\..\\cdr_small.txt"); // ścieżka względna
            //parserController.SetFilePath("D:\\Adrian Dokumenty\\Studia\\III rok\\VI semestr\\Techniki multimedialne\\Projekt\\cdr_big.txt"); // ścieżka bezwzględna
            DB.useDb = MessageController.HelloMessage();

            if (DB.useDb)
            {
                if (DB.CallRecords.Count(new BsonDocument()) == 0)
                    if (MessageController.StartLoadData())
                        parserController.Parse();
            }
            else
                parserController.Parse();


            while (true)
            {
                MessageController.MainMenuMessage();
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.F)
                {
                    //Filtruj
                    List<CallRecord> request = new List<CallRecord>();
                    if (DB.useDb)
                        request = requestController.FilterRequests();
                    else
                        request = requestController.FilterRequests(parserController.SavedDataRows);

                    parserController.SavedDataRows = request;
                    parserController.SavedDataRows.TrimExcess();
                    GC.Collect();
                }
 
                if (keyPressed.Key == ConsoleKey.R)
                {
                    //Wczytaj dane ponownie
                    if (DB.useDb)
                    {
                        DB.CallRecords.Database.DropCollection("CallRecords");
                        parserController.Parse();
                    }
                    else
                    {
                        parserController.Parse();
                        MessageController.ResetDataMessage(parserController.SavedDataRows.Count);
                    }
                }

                if (keyPressed.Key == ConsoleKey.W)
                {
                    //Pokaż informacje o danych
                    if(DB.useDb)
                        MessageController.ShowDataMessage(DB.CallRecords.Count(new BsonDocument()));
                    else
                        MessageController.ShowDataMessage(parserController.SavedDataRows.Count);
                }
                
                if(keyPressed.Key == ConsoleKey.Escape)
                {
                    //Wyjdź
                    Environment.Exit(0);
                }
            }
        }
    }
}
