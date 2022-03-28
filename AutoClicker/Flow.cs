using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    class Flow
    {
        RECT WindowRect;
        string URL = @"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images";

        public void StartFlow(RECT windowRect)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Start flow Map");

            MapWorker mapWorker = new MapWorker();
            mapWorker.RunSteps(windowRect);

            Console.WriteLine("End flow Map");


            //Console.WriteLine("\n");
            //Console.WriteLine("Start flow Expedition");

            //ExpeditionWorker expeditionWorker = new ExpeditionWorker();
            //expeditionWorker.RunSteps(windowRect);

            //Console.WriteLine("End flow Expedition");



        }

        //private void Map()
        //{
        //    //Screen.TakeScreenShot(WindowRect);

        //    //var exitScreenshot = (Bitmap)Image.FromFile(URL + @"\Assets\Map\Exit.jpg");
        //    //using (var screenshot = (Bitmap)Image.FromFile(URL + @"\Screenshot\Screenshot.jpg"))
        //    //{

        //    //    var pointClaim = new Engine().Find(screenshot, exitScreenshot);
        //    //}

        //    //var pointExit = new Engine().Find(screenshot, exitScreenshot);




        //    while (true)
        //    {

        //        Screen.TakeScreenShot(WindowRect);
        //        using (var screenshot = (Bitmap)Image.FromFile(URL + @"\Screenshot\Screenshot.jpg"))
        //        {

        //            var claimScreenshot = (Bitmap)Image.FromFile(URL + @"\Assets\Map\Claim.jpg");
        //            var pointClaim = new Engine().Find(screenshot, claimScreenshot);

        //            if (pointClaim.HasValue)
        //            {
        //                Screen.SetCursorCenterOfAction(WindowRect, claimScreenshot, pointClaim.Value);
        //                Screen.Click();
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }


        //    }

        //}
    }
}