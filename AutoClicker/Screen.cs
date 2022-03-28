using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoClicker
{

    static class Screen
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;




        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);


        public static bool TakeScreenShot(RECT rectangle, string filename = "Screenshot")
        {
            var width = rectangle.Right - rectangle.Left;
            var height = rectangle.Bottom - rectangle.Top;

            using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {

                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, new Size(width, height));
                }

                bmp.Save(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\Screenshot\" + filename + ".jpg", ImageFormat.Jpeg);
            }

            return true;
        }


        public static void SetCursorCenterOfAction(RECT window, Bitmap bitmap, Point searchResult)
        {
            var x = window.Left + searchResult.X + (bitmap.Width / 2);
            var y = window.Top + searchResult.Y + (bitmap.Height / 2);

            SetCursorPos(x, y);
        }

        public static void Click()
        {
            GetCursorPos(out Point point);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
        }
    }
}
