using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
//using System.Drawing.Bitmap;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoClicker
{




    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }


    class Program
    {
        string URL = @"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images";


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(int hWnd, string msg, string title, uint flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(int hWnd, out RECT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Point point;
            //Point pointUpdated;
            //RECT rectUpdated;
            RECT firestoneRect;
            //GetCursorPos(out point);
            //var firestoneProcess = Process.GetProcessesByName("Firestone").FirstOrDefault();
            //var firestoneHandle = firestoneProcess.MainWindowHandle.ToInt32();
            //GetWindowRect(firestoneHandle, out firestoneRect);

            //MoveWindow((IntPtr)firestoneHandle, firestoneRect.X, firestoneRect.Y, 600, 600, true);


            while (true)
            {
                //GetCursorPos(out pointUpdated);
                //TrackMousePossition(point, pointUpdated);

                //GetWindowRect(firestoneHandle, out rectUpdated);
                //TrackFirestonePossition(firestoneRect, rectUpdated);




            firestoneRect = new RECT(); 
                firestoneRect.Left = 0; 
                firestoneRect.Top = 0;
                firestoneRect.Right = 1920;
                firestoneRect.Bottom= 1080;
                var flow = new Flow();
                flow.StartFlow(firestoneRect);






                //var screenshot = (Bitmap)System.Drawing.Image.FromFile(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\test6.jpg");
                //var exitScreenshot = (Bitmap)System.Drawing.Image.FromFile(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\test7.jpg");


                //var bitmap1 = (Bitmap)System.Drawing.Image.FromFile(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\test2.jpg");

                //var sss = new Engine().Find(screenshot, exitScreenshot);

                //var sss = ImageExists(firestoneRect, bitmap1);

                Thread.Sleep(5000);
            }
        }



        public static Point? ImageExists(RECT widowRect,Bitmap needleBitmap)
        {
            Screen.TakeScreenShot(widowRect);
            using (var screenshot = (Bitmap)Image.FromFile(@"C:\Repos\AutoClicker\AutoClicker\Images\ScreenShot\test.jpg"))
            {
                var pointResult = new Engine().Find(screenshot, needleBitmap);

                return pointResult;
            }

        }

        static void TrackFirestonePossition(RECT firestoneRect, RECT pointUpdated)
        {
            if (pointUpdated.Left != firestoneRect.Left || pointUpdated.Right != firestoneRect.Right || pointUpdated.Top != firestoneRect.Top || pointUpdated.Bottom != firestoneRect.Bottom)
            {
                firestoneRect.Left = pointUpdated.Left;
                firestoneRect.Right = pointUpdated.Right;
                firestoneRect.Top = pointUpdated.Top;
                firestoneRect.Bottom = pointUpdated.Bottom;

                Console.WriteLine("top" + firestoneRect.Top + " , left" + firestoneRect.Left + " , bottom" + firestoneRect.Bottom + " , right" + firestoneRect.Right);
            }
        }

        static void TrackMousePossition(Point point, Point pointUpdated)
        {
            if (pointUpdated.X != point.X || pointUpdated.Y != point.Y)
            {
                point.X = pointUpdated.X;
                point.Y = pointUpdated.Y;

                Console.WriteLine($"Mouse active on ({point.X} ,{point.Y})");
            }

        }
    }
}
