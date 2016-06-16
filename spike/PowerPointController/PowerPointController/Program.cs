// --------------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------

namespace PowerPointController
{
    using System;

    internal class Program
    {
        public static Microsoft.Office.Interop.PowerPoint.Application GetPowerPoint()
        {
            Microsoft.Office.Interop.PowerPoint.Application instance;
            try
            {
                instance =
                    (Microsoft.Office.Interop.PowerPoint.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application");
                Console.WriteLine("Found a running instance of PowerPoint, attached.");
            }
            catch (Exception)
            {
                Console.WriteLine("No running PowerPoint instance found, starting new one.");
                instance = new Microsoft.Office.Interop.PowerPoint.Application();
                instance.Activate();
            }

            return instance;
        }

        private static void Main(string[] args)
        {
            Microsoft.Office.Interop.PowerPoint.Application powerPoint = GetPowerPoint();
            ControlPowerPoint(powerPoint);
            powerPoint.Quit();
        }

        private static void ControlPowerPoint(Microsoft.Office.Interop.PowerPoint.Application powerPoint)
        {
            Console.WriteLine("You can now control the open slide show. Press N to go to the next slide, B to go back. Q to quit.");
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Q)
                {
                    return;
                }

                Microsoft.Office.Interop.PowerPoint.SlideShowWindows slideShowWindows = powerPoint.SlideShowWindows;
                if (slideShowWindows.Count < 1) continue;

                Microsoft.Office.Interop.PowerPoint.SlideShowWindow slideShowWindow = slideShowWindows[1];

                switch (input.Key)
                {
                    case ConsoleKey.N:
                        slideShowWindow.View.Next();
                        break;
                    case ConsoleKey.B:
                        slideShowWindow.View.Previous();
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}