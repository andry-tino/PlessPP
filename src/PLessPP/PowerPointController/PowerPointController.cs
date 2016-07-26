// --------------------------------------------------------------------------
// <copyright file="PowerPointController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
namespace PowerPointController
{
    using System;

    public class PowerPointController
    {
        /// <summary>
        /// Get the running PowerPoint instance on this machine
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Control the PowerPoint presentation and make it skip a slide when a file changes
        /// </summary>
        /// <param name="powerPoint"></param>
        public static void ControlPowerPoint(Microsoft.Office.Interop.PowerPoint.Application powerPoint)
        {
            Console.WriteLine("Controlling the PowerPoint Presentation");
            DateTime lastModified = DateTime.Now;
            while (true)
            {
                try
                {
                    // Get the last date the file had been modified
                    DateTime lastFileModified =
                        System.IO.File.GetLastWriteTime(
                            @"C:\Users\codaniil\AppData\Local\Packages\c03ebf1a-2dcc-40e3-9140-ec15b2f5c21d_ph1m9x8skttmg\LocalState\Sampledata.txt");

                    // If the file has been changed more recently, then change the slide
                    if ((lastFileModified - lastModified).Seconds >=1)
                    {
                        lastModified = lastFileModified;
                        Microsoft.Office.Interop.PowerPoint.SlideShowWindows slideShowWindows =
                            powerPoint.SlideShowWindows;
                        if (slideShowWindows.Count < 1) continue;
                        Microsoft.Office.Interop.PowerPoint.SlideShowWindow slideShowWindow = slideShowWindows[1];
                        slideShowWindow.View.Next();
                    }
                }
                catch
                {
                }
            }
        }


        private static void Main(string[] args)
        {
            Microsoft.Office.Interop.PowerPoint.Application powerPoint = GetPowerPoint();
            ControlPowerPoint(powerPoint);
            powerPoint.Quit();
        }
    }
}