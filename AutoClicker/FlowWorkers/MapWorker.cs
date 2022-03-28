using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker
{
    class MapWorker
    {
        RECT WindowRect;
        string URL = @"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images";

        public bool RunSteps(RECT windowRect)
        {
            WindowRect = windowRect;
            var mapNotificationBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Map\MapNotification.jpg");
            var claimBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Map\MapClaim.jpg");
            var exitBitmap = (Bitmap)Image.FromFile(URL + @"\Assets\Map\Exit.jpg");






















            //Step 1. Map has notification
            Console.WriteLine("Map - Step 1. Check notification");
            Thread.Sleep(1000);

            var pointInHaystack = ImageExists(mapNotificationBitmap);

            if (!pointInHaystack.HasValue)
                return false;

            Screen.SetCursorCenterOfAction(WindowRect, mapNotificationBitmap, pointInHaystack.Value);
            //Screen.Click();



            //Step 2. Loop claim missions
            //Console.WriteLine("Map - Step 2. Loop claim missions");
            //Thread.Sleep(1000);
            //do
            //{
            //    pointInHaystack = ImageExists(claimBitmap);

            //    if (pointInHaystack.HasValue)
            //    {
            //        Screen.SetCursorCenterOfAction(WindowRect, claimBitmap, pointInHaystack.Value);
            //        //Screen.Click();
            //    }

            //} while (pointInHaystack.HasValue);


            //Step 3. Find missions and start them
            //do
            //{
            //    pointInHaystack = ImageExists(claimBitmap);

            //    if (pointInHaystack.HasValue)
            //    {
            //        Screen.SetCursorCenterOfAction(WindowRect, claimBitmap, pointInHaystack.Value);
            //        //Screen.Click();
            //    }

            //} while (pointInHaystack.HasValue);


            //Step 4. Exit
            //Console.WriteLine("Map - Step 4.Exit");
            //Thread.Sleep(1000);
            //do
            //{
            //    pointInHaystack = ImageExists(exitBitmap);

            //    if (pointInHaystack.HasValue)
            //    {
            //        Screen.SetCursorCenterOfAction(WindowRect, exitBitmap, pointInHaystack.Value);
            //        //Screen.Click();
            //    }

            //} while (pointInHaystack.HasValue);


            return true;

        }


        public Point? ImageExists(Bitmap needleBitmap)
        {
            Screen.TakeScreenShot(WindowRect);
            using (var screenshot = (Bitmap)Image.FromFile(URL + @"\Screenshot\Screenshot.jpg"))
            {
                var pointResult = new Engine2().Find(screenshot, needleBitmap);

                return pointResult;
            }

        }
    }
}



//var result = new byte[(mapNotificationBitmap.Width * mapNotificationBitmap.Height)];

//var bitmapData = mapNotificationBitmap.LockBits(new Rectangle(0, 0, mapNotificationBitmap.Width, mapNotificationBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
//int numbytes = bitmapData.Stride * bitmapData.Height;
//IntPtr ptr = bitmapData.Scan0;
//Marshal.Copy(ptr, result, 0, numbytes);



//mapNotificationBitmap.UnlockBits(bitmapData);






//unsafe
//{
//    fixed (byte* ptrr = result)
//    {
//        using (Bitmap image = new Bitmap(bitmapData.Width, bitmapData.Height, bitmapData.Stride, PixelFormat.Format1bppIndexed, new IntPtr(ptrr)))
//        {

//            image.Save(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\11111111111.bmp");
//        }
//    }
//}








//var result2 = new byte[mapNotificationBitmap.Height][];


//var result = new byte[(mapNotificationBitmap.Width * mapNotificationBitmap.Height)];

//var bitmapData = mapNotificationBitmap.LockBits(new Rectangle(0, 0, mapNotificationBitmap.Width, mapNotificationBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
//int numbytes = bitmapData.Stride * bitmapData.Height;
//IntPtr ptr = bitmapData.Scan0;
//Marshal.Copy(ptr, result, 0, numbytes);
//mapNotificationBitmap.UnlockBits(bitmapData);


//var count = 0;
//for (int y = 0; y < bitmapData.Height; y++)
//{
//    result2[y] = new byte[bitmapData.Width];
//    for (int x = 0; x < bitmapData.Width; x++)
//    {
//        result2[y][x] = result[count];
//        count++;
//    }


//}


//List<byte> arrayLst = new List<byte>();

//for (int i = 0; i < result2.Length; i++)
//    for (int j = 0; j < result2[i].Length; j++)
//        arrayLst.Add(result2[i][j]);

//var bitmapArr = arrayLst.ToArray();









//unsafe
//{
//    fixed (byte* ptrr = bitmapArr)
//    {
//        using (Bitmap image = new Bitmap(bitmapData.Width, bitmapData.Height, bitmapData.Stride, PixelFormat.Format1bppIndexed, new IntPtr(ptrr)))
//        {

//            image.Save(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\11111111111.png");
//        }
//    }
//}
