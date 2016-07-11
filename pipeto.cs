/*****************************************************************************
/* Copyright (c) 2016 xanthalas.co.uk                                       */
/*                                                                          */
/* Author: Xanthalas                                                        */
/* Date  : July 2016                                                        */
/*                                                                          */
/*  This file is part of pipeto.                                            */
/*                                                                          */
/*  pipeto is free software: you can redistribute it and/or modify          */
/*  it under the terms of the GNU General Public License as published by    */
/*  the Free Software Foundation, either version 3 of the License, or       */
/*  (at your option) any later version.                                     */
/*                                                                          */
/*  pipeto is distributed in the hope that it will be useful,               */
/*  but WITHOUT ANY WARRANTY; without even the implied warranty of          */
/*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the           */
/*  GNU General Public License for more details.                            */
/*                                                                          */
/*  You should have received a copy of the GNU General Public License       */
/*  along with pipeto.  If not, see <http://www.gnu.org/licenses/>.         */
/*                                                                          */
/****************************************************************************/
using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace Xanthalas
{
    public class PipeTo
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No application given. Run pipeto -h for help");
                return;
            }

            if (args.Length == 1 && (args[0] == "-h" || args[0] == "--help" || args[0] == "/?"))
            {
                Console.WriteLine("PipeTo v 1.0 (c) Xanthalas 2016");
                Console.WriteLine("\nReads stdin and writes it to a temporary file and then opens the file in the application given.");
                Console.WriteLine("Usage: command | pipeto application");
                Console.WriteLine("For example: dir | pipeto notepad");
                return;
            }
            var tempFilename = Path.GetTempFileName();

            System.Console.WriteLine("Writing stdin to file " + tempFilename);

            using (StreamWriter writer = new StreamWriter(tempFilename))
            {
                string line;

                while ((line = Console.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }
            }

            if (args.Length > 0)
            {
                var command = args[0];
                StringBuilder parmsString = new StringBuilder();
   
                if (args.Length > 1)
                {
                    int counter = 0;
                    foreach (var item in args)
                    {
                        counter++;
                        if (counter > 1)
                        {
                            parmsString.Append(item + " ");
                        }
                    }
                }

                parmsString.Append(tempFilename);

                try
                {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.FileName = command;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Arguments = parmsString.ToString();
                    proc.Start();                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Couldn't start application " + command);
                    Console.WriteLine("Error was: " + e.Message);
                }

            }
        }

    }
}