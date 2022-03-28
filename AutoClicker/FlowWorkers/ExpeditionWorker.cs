using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker
{
    class ExpeditionWorker
    {
        RECT WindowRect;
        string URL = @"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images";

        public bool RunSteps(RECT windowRect)
        {
            WindowRect = windowRect;
            var expeditionsNotificationBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\MapNotification.jpg");
            var startExpeditionBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\MapClaim.jpg");
            var OKBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\ExpeditionsOK.jpg");
            var claimBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\ExpeditionsClaim.jpg");
            var exitBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\ExpeditionsExit.jpg");
            //var exitBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Expeditions\Exit.jpg");



            //Step 1. Expedition has notification
            Console.WriteLine("Expedition - Step 1. Notification check");
            //Thread.Sleep(1000);

            var pointInHaystack = ImageExists(expeditionsNotificationBitmap);

            if (!pointInHaystack.HasValue)
                return false;

            Screen.SetCursorCenterOfAction(WindowRect, expeditionsNotificationBitmap, pointInHaystack.Value);
            //Screen.Click();



            //Step 2. Claim Expedition if any
            Console.WriteLine("Expedition - Step 2. Claim if any");
            Thread.Sleep(1000);

            pointInHaystack = ImageExists(claimBitmap);

            if (pointInHaystack.HasValue)
            {
                Screen.SetCursorCenterOfAction(WindowRect, claimBitmap, pointInHaystack.Value);
                //Screen.Click();

                pointInHaystack = ImageExists(OKBitmap);
                if (pointInHaystack.HasValue)
                {
                    Screen.SetCursorCenterOfAction(WindowRect, OKBitmap, pointInHaystack.Value);
                    //Screen.Click();

                }
            }



            //Step 3. Start expedition
            Console.WriteLine("Expedition - Step 3. Start expedition");
            Thread.Sleep(1000);
            pointInHaystack = ImageExists(startExpeditionBitmap);

            if (pointInHaystack.HasValue)
            {
                Screen.SetCursorCenterOfAction(WindowRect, startExpeditionBitmap, pointInHaystack.Value);
                //Screen.Click();
            }



            //Step 4. Exit
            Console.WriteLine("Expedition - Step 4.Exit");
            Thread.Sleep(1000);
            do
            {
                pointInHaystack = ImageExists(exitBitmap);

                if (pointInHaystack.HasValue)
                {
                    Screen.SetCursorCenterOfAction(WindowRect, exitBitmap, pointInHaystack.Value);
                    //Screen.Click();
                }

            } while (pointInHaystack.HasValue);


            return true;
        }


        public Point? ImageExists(Bitmap needleBitmap)
        {
            Screen.TakeScreenShot(WindowRect);
            using (var screenshot = (Bitmap)Image.FromFile(URL + @"\Screenshot\Screenshot.jpg"))
            {
                var pointResult = new Engine().Find(screenshot, needleBitmap);

                return pointResult;
            }

        }
    }
}